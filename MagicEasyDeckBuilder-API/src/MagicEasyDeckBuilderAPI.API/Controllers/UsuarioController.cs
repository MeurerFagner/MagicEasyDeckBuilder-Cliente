using MagicEasyDeckBuilderAPI.API.Configuration;
using MagicEasyDeckBuilderAPI.App.Services;
using MagicEasyDeckBuilderAPI.App.ViewModel;
using MagicEasyDeckBuilderAPI.Dominio.Entidades;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MagicEasyDeckBuilderAPI.API.Controllers
{
    [ApiController]
    [Route("usuario")]
    public class UsuarioController: ControllerBase
    {
        private readonly IUsuarioAppService _app;
        private readonly TokenService _tokenService;

        public UsuarioController(IUsuarioAppService app, TokenService tokenService)
        {
            _app = app;
            _tokenService = tokenService;
        }

        [HttpPost, Route("login")]
        public async Task<IActionResult> Login(UsuarioLoginViewModel model)
        {
            var usuario = await _app.ObtemUsuario(model.Email,model.Senha);

            if (usuario == null)
                return NotFound("Login ou senha inválidos.");

            var token = _tokenService.GenerateToken(usuario);

            return Ok(new UsuarioAuthViewModel
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Token = token
            });
        }

        [HttpPost, Route("cadastro")]
        public async Task<IActionResult> Cadastrar(UsuarioCadastroViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest("model inválido");

           var result = await _app.Cadastrar(model.Nome, model.Email, model.Senha);

            if (result.Sucesso) 
            {
                var usuario = await _app.ObtemUsuario(model.Email, model.Senha);
                var token = _tokenService.GenerateToken(usuario);

                var retorno = new UsuarioAuthViewModel
                {
                    Id = usuario.Id,
                    Nome = usuario.Nome,
                    Email = usuario.Email,
                    Token = token
                };
                return Ok(retorno);
            }

            return BadRequest(result.Mensagem);
        }
    }
}
