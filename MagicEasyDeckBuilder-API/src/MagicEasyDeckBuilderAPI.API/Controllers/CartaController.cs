using MagicEasyDeckBuilderAPI.App.Services;
using MagicEasyDeckBuilderAPI.Core.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MagicEasyDeckBuilderAPI.API.Controllers
{
    [ApiController]
    [Route("cartas")]
    public class CartaController: ControllerBase
        
    {
        private readonly ICartaAppService _app;

        public CartaController(ICartaAppService app)
        {
            _app = app;
        }

        [HttpGet]
        public async Task<IActionResult> BuscaCartas([FromBody] FiltroBuscaCartaViewModel filtros)
        {
            var cartas = await _app.BuscaCartas(filtros);

            return Ok(cartas);
        }

        [HttpGet]
        [Route("nomes/{nome}")]
        public async Task<IActionResult> BuscaNomesDeCarta(string nome)
        {
            var cartas = await _app.BuscaNomesDeCarta(nome);

            return Ok(cartas);
        }

        [HttpGet]
        [Route("{nome}")]
        public async Task<IActionResult> BuscaCartaPorNome(string nome)
        {
            var carta = await _app.BuscaCartaPorNome(nome);

            return Ok(carta);
        }

        [HttpGet]
        [Route("tipos")]
        public async Task<IActionResult> BuscaTipos()
        {
            var tipos = await _app.BuscaTipos();

            return Ok(tipos);
        }

        [HttpGet]
        [Route("edicoes")]
        public async Task<IActionResult> BuscaEdicoes()
        {
            var edicoes = await _app.BuscaEdicoes();

            return Ok(edicoes);
        }

    }
}
