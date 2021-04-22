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

        public async Task<ResultResponseModel> CriarUsuario(Usuario usuario)
        {
            usuario.senha = Hash.Create(HashType.SHA256, usuario.senha, DataDictionary.CHAVE_ENCRIPTACAO, false);

            var idUsuarioCadastrado = await _usuarioRepository.CreateAsync(usuario);

            if (idUsuarioCadastrado == 0) return new ResultResponseModel(true, "Erro ao cadastrar usuário");

            return new ResultResponseModel(false, "Cadastro realizado com sucesso");
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
