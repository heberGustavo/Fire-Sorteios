using Effortless.Net.Encryption;
using Sorteio.Common;
using Sorteio.Domain.Business.Base;
using Sorteio.Domain.IBusiness;
using Sorteio.Domain.IRepository;
using Sorteio.Domain.IRepository.Base;
using Sorteio.Domain.Models.Body;
using Sorteio.Domain.Models.Common;
using Sorteio.Domain.Models.EntityDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorteio.Domain.Business
{
    public class UsuarioBusiness : BusinessBase<Usuario>, IUsuarioBusiness
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ISorteiosRepository _sorteiosRepository;

        public UsuarioBusiness(IUsuarioRepository usuarioRepository, ISorteiosRepository sorteiosRepository) : base(usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
            _sorteiosRepository = sorteiosRepository;
        }

        public async Task<ResultResponseModel<Usuario>> CadastrarUsuarioCadastrarNumeros(CadastrarUsuarioListaNumerosBody body)
        {
            var usuarioExistente = await _usuarioRepository.GetAllAsync(x => x.id_tipo_usuario != DataDictionary.USUARIO_ADMINISTRADOR);

            foreach (var item in usuarioExistente)
            {
                if (item.email.Equals(body.usuario.email, StringComparison.OrdinalIgnoreCase))
                {
                    return new ResultResponseModel<Usuario>(true, "Email já cadastrado", null);
                }
                if (item.cpf.Equals(body.usuario.cpf, StringComparison.OrdinalIgnoreCase))
                {
                    return new ResultResponseModel<Usuario>(true, "CPF já cadastrado", null);
                }
            }

            body.usuario.id_tipo_usuario = DataDictionary.USUARIO_CLIENTE;
            body.usuario.senha = Hash.Create(HashType.SHA256, body.usuario.senha, DataDictionary.CHAVE_ENCRIPTACAO, false);

            if (body.usuario.data_de_nascimento.Year <= 1760)
            {
                body.usuario.data_de_nascimento = new DateTime(1760, 01, 01);
            }

            var idUsuarioCadastrado = await _usuarioRepository.CreateAsync(body.usuario);

            if (idUsuarioCadastrado == 0) return new ResultResponseModel<Usuario>(true, "Erro ao cadastrar usuário", null);

            var cadastrarNumerosEscolhidos = await _sorteiosRepository.CadastrarNumerosEscolhidos(body.valor_total, body.numeroSorteios, idUsuarioCadastrado, body.id_sorteio);

            return new ResultResponseModel<Usuario>(false, "Cadastro realizado com sucesso", null);

        }

        public async Task<ResultResponseModel> CriarUsuario(Usuario usuario)
        {
            usuario.senha = Hash.Create(HashType.SHA256, usuario.senha, DataDictionary.CHAVE_ENCRIPTACAO, false);

            var idUsuarioCadastrado = await _usuarioRepository.CreateAsync(usuario);

            if (idUsuarioCadastrado == 0) return new ResultResponseModel(true, "Erro ao cadastrar usuário");

            return new ResultResponseModel(false, "Cadastro realizado com sucesso");
        }

        public Task<int> EditarDadosCliente(Usuario usuario)
        {
            if(usuario.data_de_nascimento.Year <= 1760)
            {
                usuario.data_de_nascimento = new DateTime(1760, 01, 01);
            }

            var resultado = _usuarioRepository.EditarDadosCliente(usuario);
            return resultado;
        }

        public async Task<ResultResponseModel<Usuario>> LogarCadastraNumeros(LoginListaNumerosBody login)
        {
            var usuarios = await _usuarioRepository.GetAllAsync();
            login.senha = Hash.Create(HashType.SHA256, login.senha, DataDictionary.CHAVE_ENCRIPTACAO, false);

            var usuarioCadastrado = usuarios.FirstOrDefault(u => u.email == login.email && u.senha == login.senha);

            if (usuarioCadastrado == null) return new ResultResponseModel<Usuario>(true, "Login/Senha Inválidos", null);

            var cadastrarNumerosEscolhidos = await _sorteiosRepository.CadastrarNumerosEscolhidos(login.valor_total, login.numeroSorteios, usuarioCadastrado.id_usuario, login.id_sorteio);


            return new ResultResponseModel<Usuario>(false, "Sucesso", usuarioCadastrado);
        }

        public Task<IEnumerable<Usuario>> ObterTodosUsuarios()
            => _usuarioRepository.GetAllAsync();

        public async Task<ResultResponseModel<Usuario>> RealizarLogin(string email, string senha)
        {
            var usuarios = await _usuarioRepository.GetAllAsync();

            senha = Hash.Create(HashType.SHA256, senha, DataDictionary.CHAVE_ENCRIPTACAO, false);

            var usuarioCadastrado = usuarios.FirstOrDefault(u => u.email == email && u.senha == senha);

            if (usuarioCadastrado == null) return new ResultResponseModel<Usuario>(true, "Login/Senha Inválidos", null);

            return new ResultResponseModel<Usuario>(false, "Sucesso", usuarioCadastrado);
        }
    }
}
