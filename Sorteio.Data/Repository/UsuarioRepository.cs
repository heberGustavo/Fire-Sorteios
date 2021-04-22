using AutoMapper;
using Dapper;
using Sorteio.Data.EntityData;
using Sorteio.Data.Repository.Base;
using Sorteio.Domain.IRepository;
using Sorteio.Domain.Models.Common;
using Sorteio.Domain.Models.EntityDomain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sorteio.Data.Repository
{
    public class UsuarioRepository : RepositoryBase<Usuario, UsuarioData>, IUsuarioRepository
    {
        public UsuarioRepository(SqlDataContext dataContext, IMapper mapper) : base(dataContext, mapper)
        {
        }

        public async Task<int> EditarDadosCliente(Usuario usuario)
            => await _dataContext.Connection.ExecuteAsync(@"UPDATE Usuario SET nome = @nome,
                                                                    cpf = @cpf,
                                                                    data_de_nascimento = @data_de_nascimento,
                                                                    celular = @celular,
                                                                    cep = @cep,
                                                                    logadouro = @logadouro,
                                                                    numero = @numero,
                                                                    bairro = @bairro,
                                                                    complemento = @complemento,
                                                                    estado = @estado,
                                                                    cidade = @cidade,
                                                                    referencia = @referencia
                                                                    WHERE id_usuario = @id_usuario", new 
                                                                { 
                                                                    nome = usuario.nome,
                                                                    cpf = usuario.cpf,
                                                                    data_de_nascimento = usuario.data_de_nascimento,
                                                                    celular = usuario.celular,
                                                                    cep = usuario.cep,
                                                                    logadouro = usuario.logadouro,
                                                                    numero = usuario.numero,
                                                                    bairro = usuario.bairro,
                                                                    complemento = usuario.complemento,
                                                                    estado = usuario.estado,
                                                                    cidade = usuario.cidade,
                                                                    referencia = usuario.referencia,
                                                                    id_usuario = usuario.id_usuario
                                                                });
    }
}
