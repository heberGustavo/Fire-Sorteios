using AutoMapper;
using Dapper;
using Dapper.Contrib.Extensions;
using Sorteio.Data.EntityData;
using Sorteio.Data.Repository.Base;
using Sorteio.Domain.IRepository;
using Sorteio.Domain.Models.Body;
using Sorteio.Domain.Models.Common;
using Sorteio.Domain.Models.EntityDomain;
using Sorteio.Domain.Models.NotMapped;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sorteio.Data.Repository
{
    public class SorteiosRepository : RepositoryBase<Domain.Models.EntityDomain.Sorteio, Data.EntityData.SorteioData>, ISorteiosRepository
    {
        public SorteiosRepository(SqlDataContext dataContext, IMapper mapper) : base(dataContext, mapper)
        {
        }

        public async Task<ResultResponseModel> CriarNovoSorteio(SorteioBody sorteioBody)
        {
            using (var dbContextTransaction = _dataContext.Connection.BeginTransaction())
            {
                try
                {
                    var idSorteioCadatrado = await _dataContext.Connection.InsertAsync(_mapper.Map<Sorteio.Data.EntityData.SorteioData>(sorteioBody.sorteio), dbContextTransaction);

                    foreach (var itemImagem in sorteioBody.linkImagens)
                    {
                        itemImagem.id_sorteio = idSorteioCadatrado;

                        await _dataContext.Connection.InsertAsync(_mapper.Map<GaleriaFotosData>(itemImagem), dbContextTransaction);
                    }

                    dbContextTransaction.Commit();

                    return new ResultResponseModel(false, "Sorteio cadastrado com sucesso");
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    return new ResultResponseModel(true, "Erro ao cadastrar Sorteio, entre em contato com o suporte.");
                }
            }
        }

        public async Task<ResultResponseModel> FinalizarSorteio(VencedorSorteio vencedorSorteio)
        {
            using (var dbContextTransaction = _dataContext.Connection.BeginTransaction())
            {
                try
                {
                    var idVencedorSorteioCadastrado = await _dataContext.Connection.InsertAsync(_mapper.Map<VencedorSorteioData>(vencedorSorteio), dbContextTransaction);

                    await _dataContext.Connection.ExecuteAsync(@"UPDATE Sorteio
                                                                 SET status = 1
                                                                 WHERE id_sorteio = @idSorteio",
                                                                 new { idSorteio = vencedorSorteio.id_sorteio },
                                                                 dbContextTransaction);

                    dbContextTransaction.Commit();

                    return new ResultResponseModel(false, "Sorteio finalizado com sucesso");
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    return new ResultResponseModel(true, "Erro ao finalizar Sorteio, entre em contato com o suporte.");
                }
            }
        }

        public Task<IEnumerable<SorteioNotMapped>> ObterTodosSorteio()
            => _dataContext.Connection.QueryAsync<SorteioNotMapped>(@"SELECT s.id_sorteio, u.id_usuario, s.nome as nome_sorteio, s.edicao as edicao_sorteio, s.status, u.nome as nome_ganhador
                                                                      FROM Sorteio s 
                                                                      LEFT JOIN VencedorSorteio vs ON s.id_sorteio = vs.id_sorteio 
                                                                      LEFT JOIN Usuario u ON vs.id_usuario = u.id_usuario");
    }
}
