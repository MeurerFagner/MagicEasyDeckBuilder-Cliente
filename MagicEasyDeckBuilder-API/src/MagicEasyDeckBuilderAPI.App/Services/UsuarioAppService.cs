using MagicEasyDeckBuilderAPI.Dominio.DTO;
using MagicEasyDeckBuilderAPI.Dominio.Entidades;
using MagicEasyDeckBuilderAPI.Dominio.Interfaces.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicEasyDeckBuilderAPI.App.Services
{
    public class UsuarioAppService: IUsuarioAppService
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioAppService(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        public async Task<RetornoDTO> Cadastrar(string nome, string email, string senha)
        {
            var usuarioEmail = await _repository.ObtemUsuarioPorEmail(email);

            if (usuarioEmail != null)
            {
                return new RetornoDTO
                {
                    Sucesso = false,
                    Mensagem = "E-mail já está em uso",
                };
            }

            await _repository.Cadastrar(nome, email, senha);
            await _repository.UnitOfWork.Commit();

            return new RetornoDTO { Sucesso = true };
        }

        public async Task<Usuario> ObtemUsuario(string login, string senha)
        {
            var usuario = await _repository.ObtemUsuarioPorEmailESenha(login, senha);

            return usuario;
        }
    }
}
