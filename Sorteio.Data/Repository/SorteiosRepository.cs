using AutoMapper;
using Dapper;
using Dapper.Contrib.Extensions;
using Sorteio.Common;
using Sorteio.Data.EntityData;
using Sorteio.Data.Repository.Base;
using Sorteio.Domain.IRepository;
using Sorteio.Domain.Models.Body;
using Sorteio.Domain.Models.Common;
using Sorteio.Domain.Models.EntityDomain;
using Sorteio.Domain.Models.NotMapped;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorteio.Data.Repository
{
    public class SorteiosRepository : RepositoryBase<Domain.Models.EntityDomain.Sorteio, Data.EntityData.SorteioData>, ISorteiosRepository
    {
        public SorteiosRepository(SqlDataContext dataContext, IMapper mapper) : base(dataContext, mapper)
        {
        }

        public async Task<bool> CadastrarNumerosEscolhidos(decimal valorTotal, IEnumerable<NumeroEscolhido> numeroSorteios, int idUsuario, int idSorteio)
        {
            using (var dbContextTransaction = _dataContext.Connection.BeginTransaction())
            {
                try
                {
                    var pedidoData = new PedidoData()
                    {
                        id_usuario = idUsuario,
                        id_sorteio = idSorteio,
                        data_pedido = DateTime.Now,
                        valor_total = valorTotal,
                        id_status_pedido = DataDictionary.STATUS_PEDIDO_PENDENTE
                    };

                    var idPedidoCadastrado = await _dataContext.Connection.InsertAsync(pedidoData, dbContextTransaction);

                    foreach (var itemNumeros in numeroSorteios)
                    {
                        var numeroEscolhidoData = new NumeroEscolhidoData
                        {
                            id_pedido = idPedidoCadastrado,
                            numero = itemNumeros.numero
                        };

                        await _dataContext.Connection.InsertAsync(numeroEscolhidoData, dbContextTransaction);
                    }

                    dbContextTransaction.Commit();

                    return true;
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    return false;
                }
            }
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

        public async Task<bool> EditarFinalizarSorteio(VencedorSorteio body)
            => await _dataContext.Connection.UpdateAsync(_mapper.Map<VencedorSorteioData>(body));

        public async Task<ResultResponseModel> EditarSorteio(SorteioBody body)
        {
            using (var dbContextTransaction = _dataContext.Connection.BeginTransaction())
            {
                try
                {
                    await _dataContext.Connection.UpdateAsync(_mapper.Map<Sorteio.Data.EntityData.SorteioData>(body.sorteio), dbContextTransaction);

                    if(body.linkImagens.Count() > 0) //Se existir imagem para esse sorteio, apaga todas antes de cadastrar as novas
                    {
                        await _dataContext.Connection.ExecuteAsync(@"DELETE FROM GaleriaFotos WHERE id_sorteio = @idSorteio", new { idSorteio = body.sorteio.id_sorteio }, dbContextTransaction);
                    }

                    foreach (var itemImagem in body.linkImagens)
                    {
                        itemImagem.id_sorteio = body.sorteio.id_sorteio;

                        await _dataContext.Connection.InsertAsync(_mapper.Map<GaleriaFotosData>(itemImagem), dbContextTransaction);
                    }

                    dbContextTransaction.Commit();

                    return new ResultResponseModel(false, "Sorteio atualizado com sucesso!");
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    return new ResultResponseModel(true, "Erro ao atualizar Sorteio, entre em contato com o suporte.");
                }
            }
        }

        public Task<int> ExcluirSorteio(int idSorteio)
            => _dataContext.Connection.ExecuteAsync(@"UPDATE Sorteio SET excluido = 1 WHERE id_sorteio = @idSorteio", new { idSorteio });

        public Task<IEnumerable<InformacoesSorteio>> FiltrarSorteioPorCategoria(int idCategoria)
             => _dataContext.Connection.QueryAsync<InformacoesSorteio>(@"SELECT s.id_sorteio, s.nome, s.edicao, s.valor, s.quantidade_numeros, s.status, 
                                                                        vs.numero_sorteado, vs.data_sorteio, 
                                                                        u.nome as nome_ganhador, 
                                                                        (
	                                                                        SELECT TOP 1 gf.url_imagem 
	                                                                        FROM GaleriaFotos gf  
	                                                                        WHERE gf.id_sorteio = s.id_sorteio 
	                                                                        ORDER BY gf.url_imagem
                                                                        ) as url_imagem 
                                                                        FROM Sorteio s 
                                                                        LEFT JOIN VencedorSorteio vs ON s.id_sorteio = vs.id_sorteio 
                                                                        LEFT JOIN Usuario u ON vs.id_usuario = u.id_usuario
                                                                        WHERE s.id_categoria_sorteio = @idCategoria AND s.status = 1 AND s.excluido = 0
                                                                        ORDER BY s.edicao + 0 ASC", 
                                                                        new { idCategoria });

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

        public async Task<SorteioBody> ObterDadosDoSorteioPorId(int idSorteio)
        {
            try
            {
                var results = _dataContext.Connection.QueryMultiple(@"
                                                                        SELECT * FROM Sorteio s WHERE s.id_sorteio = @idSorteio
                                                                        SELECT * FROM GaleriaFotos gf WHERE gf.id_sorteio = @idSorteio
                                                                     ", new { idSorteio });

                var sorteio = results.ReadSingleOrDefault<Sorteio.Domain.Models.EntityDomain.Sorteio>();
                var imagens = results.Read<GaleriaFotos>();

                var dados = new SorteioBody(sorteio, imagens);

                return dados;

            }
            catch(Exception e)
            {
                return null;
            }
        }

        public Task<IEnumerable<InformacoesSorteio>> ObterInformacoesSorteio()
            => _dataContext.Connection.QueryAsync<InformacoesSorteio>(@"SELECT s.id_sorteio, s.nome, s.edicao, s.valor, s.quantidade_numeros, s.status,
                                                                        vs.numero_sorteado, vs.data_sorteio, 
                                                                        u.nome as nome_ganhador,
                                                                        (SELECT TOP 1 gf.url_imagem 
	                                                                        FROM GaleriaFotos gf  
	                                                                        WHERE gf.id_sorteio = s.id_sorteio 
	                                                                        ORDER BY gf.url_imagem) as url_imagem 
                                                                        FROM Sorteio s
                                                                        LEFT JOIN VencedorSorteio vs ON s.id_sorteio = vs.id_sorteio 
                                                                        LEFT JOIN Usuario u ON vs.id_usuario = u.id_usuario
                                                                        WHERE s.excluido = 0
                                                                        ORDER BY s.edicao + 0 ASC");

        public Task<IEnumerable<MeusPremios>> ObterMeusPremiosClientePorId(int idUsuario)
            => _dataContext.Connection.QueryAsync<MeusPremios>(@"SELECT s.nome as nome_sorteio, vs.data_sorteio, vs.numero_sorteado 
                                                                 FROM VencedorSorteio vs 
                                                                 LEFT JOIN Sorteio s ON vs.id_sorteio = s.id_sorteio 
                                                                 WHERE vs.id_usuario = @idUsuario
                                                                 ORDER BY vs.data_sorteio DESC", new { idUsuario });

        public Task<IEnumerable<NumeroEscolhidoBody>> ObterNumerosDoSorteioPorId(int idSorteio)
            => _dataContext.Connection.QueryAsync<NumeroEscolhidoBody>(@"SELECT ne.*, p.id_status_pedido, u.nome as nome_usuario 
                                                                         FROM Pedido p 
                                                                         LEFT JOIN NumeroEscolhido ne ON p.id_pedido = ne.id_pedido 
                                                                         LEFT JOIN Usuario u ON u.id_usuario = p.id_usuario 
                                                                         WHERE p.id_sorteio = @idSorteio", new { idSorteio });

        public Task<IEnumerable<ParticipanteSorteio>> ObterParticipantesSorteioPorId(int idSorteio)
            => _dataContext.Connection.QueryAsync<ParticipanteSorteio>(@"SELECT u.id_usuario, u.nome, u.cpf, ne.numero, p.id_status_pedido, p.id_pedido
                                                                         FROM Usuario u 
                                                                         LEFT JOIN Pedido p ON u.id_usuario = p.id_usuario 
                                                                         LEFT JOIN NumeroEscolhido ne ON p.id_pedido = ne.id_pedido
                                                                         WHERE p.id_sorteio = @idSorteio;", new { idSorteio });

        public async Task<SorteioNotMapped> ObterSorteioPorId(int idSorteio)
            => await _dataContext.Connection.QueryFirstOrDefaultAsync<SorteioNotMapped>(@"SELECT COUNT(gf.id_galeria_fotos) quantidade_imagens, s.id_sorteio, s.id_categoria_sorteio, s.nome as nome_sorteio, s.edicao as edicao_sorteio, s.valor, s.quantidade_numeros, s.descricao_curta, s.descricao_longa, s.status,  
                                                                                          vs.id_usuario, vs.id_vencedor_sorteio, u.nome as nome_ganhador, vs.numero_sorteado, vs.data_sorteio
                                                                                          FROM Sorteio s
                                                                                          LEFT JOIN VencedorSorteio vs ON s.id_sorteio = vs.id_sorteio 
                                                                                          LEFT JOIN Usuario u ON vs.id_usuario = u.id_usuario
                                                                                          LEFT JOIN GaleriaFotos gf ON s.id_sorteio = gf.id_sorteio 
                                                                                          WHERE s.id_sorteio = @idSorteio
                                                                                          GROUP BY s.id_sorteio, s.id_categoria_sorteio, s.nome, s.edicao, s.valor, s.quantidade_numeros, s.descricao_curta, s.descricao_longa, s.status,  
                                                                                          vs.id_usuario, vs.id_vencedor_sorteio, u.nome, vs.numero_sorteado, vs.data_sorteio", new { idSorteio = idSorteio } );

        public Task<IEnumerable<MeusBilhetes>> ObterSorteiosBilheteClientePorId(int idUsuario)
            => _dataContext.Connection.QueryAsync<MeusBilhetes>(@"SELECT p.id_pedido, s.nome as nome_sorteio, p.data_pedido as data_compra_sorteio, p.id_status_pedido, ne.numero, s.valor 
                                                                  FROM Pedido p 
                                                                  LEFT JOIN Sorteio s ON p.id_sorteio = s.id_sorteio
                                                                  LEFT JOIN NumeroEscolhido ne ON ne.id_pedido = p.id_pedido
                                                                  WHERE p.id_usuario = @idUsuario
                                                                  ORDER BY p.data_pedido DESC", new { idUsuario });

        public Task<IEnumerable<SorteioNotMapped>> ObterTodosSorteio()
            => _dataContext.Connection.QueryAsync<SorteioNotMapped>(@"SELECT s.id_sorteio, u.id_usuario, s.nome as nome_sorteio, s.edicao as edicao_sorteio, s.status, u.nome as nome_ganhador
                                                                      FROM Sorteio s 
                                                                      LEFT JOIN VencedorSorteio vs ON s.id_sorteio = vs.id_sorteio 
                                                                      LEFT JOIN Usuario u ON vs.id_usuario = u.id_usuario
                                                                      WHERE s.excluido = 0
                                                                      ORDER BY s.edicao + 0 ASC");

        public Task<IEnumerable<InformacoesSorteio>> ObterTodosUltimosSorteiosRealizados()
            => _dataContext.Connection.QueryAsync<InformacoesSorteio>(@"SELECT s.id_sorteio, s.nome, s.edicao, s.valor, s.quantidade_numeros, s.status, 
                                                                        vs.numero_sorteado, vs.data_sorteio, 
                                                                        u.nome as nome_ganhador,
                                                                        (
	                                                                        SELECT TOP 1 gf.url_imagem 
	                                                                        FROM GaleriaFotos gf 
	                                                                        WHERE gf.id_sorteio = s.id_sorteio 
	                                                                        ORDER BY gf.url_imagem ASC 
                                                                        ) as url_imagem
                                                                        FROM Sorteio s 
                                                                        LEFT JOIN VencedorSorteio vs ON s.id_sorteio = vs.id_sorteio 
                                                                        LEFT JOIN Usuario u ON vs.id_usuario = u.id_usuario
                                                                        WHERE s.status = 1 AND s.excluido = 0
                                                                        ORDER BY s.edicao + 0 ASC");
    }
}
