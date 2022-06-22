using MagicEasyDeckBuilderAPI.Core.Data;
using MagicEasyDeckBuilderAPI.Core.Helpers;
using MagicEasyDeckBuilderAPI.Dominio.Entidades;
using MagicEasyDeckBuilderAPI.Dominio.Interfaces.Infra;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicEasyDeckBuilderAPI.Infra.Repositorio
{
    public class UsuarioRepository : BaseRepository, IUsuarioRepository
    {
        private const string HASH_KEY = "magic_builder_hash_senha";

        public UsuarioRepository(Context context) :base(context)
        {
        }


        public async Task Cadastrar(string nome, string email, string senha)
        {
            var usuario = new Usuario
            {
                Nome = nome,
                Email = email,
                Senha = StringHelper.HashString(senha,HASH_KEY)
            };

            await _context.AddAsync(usuario);
        }

        public async Task<Usuario> ObtemUsuarioPorEmail(string email)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);

            return usuario;
        }

        public async Task<Usuario> ObtemUsuarioPorEmailESenha(string email, string senha)
        {
            var senhaHash = StringHelper.HashString(senha, HASH_KEY);

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(
                u => u.Email == email &&
                     u.Senha == senhaHash);

            return usuario;

        }
    }
}
