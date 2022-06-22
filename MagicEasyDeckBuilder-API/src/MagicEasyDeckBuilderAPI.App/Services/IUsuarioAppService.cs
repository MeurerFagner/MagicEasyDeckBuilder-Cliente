using MagicEasyDeckBuilderAPI.Dominio.DTO;
using MagicEasyDeckBuilderAPI.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicEasyDeckBuilderAPI.App.Services
{
    public interface IUsuarioAppService
    {
        Task<Usuario> ObtemUsuario(string login, string senha);
        Task<RetornoDTO> Cadastrar(string nome, string email, string senha);
    }
}
