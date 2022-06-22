using MagicEasyDeckBuilderAPI.Core.Data;
using MagicEasyDeckBuilderAPI.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicEasyDeckBuilderAPI.Dominio.Interfaces.Infra
{
    public interface IUsuarioRepository : IRepository
    {
        Task<Usuario> ObtemUsuarioPorEmail(string email);
        Task Cadastrar(string nome, string email, string senha);
        Task<Usuario> ObtemUsuarioPorEmailESenha(string email, string senha);
    }
}
