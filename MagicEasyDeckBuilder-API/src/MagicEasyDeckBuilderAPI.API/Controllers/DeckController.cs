using MagicEasyDeckBuilderAPI.App.Services;
using MagicEasyDeckBuilderAPI.Core.ViewModel;
using MagicEasyDeckBuilderAPI.Dominio.Entidades;
using MagicEasyDeckBuilderAPI.Dominio.ObjetoDeValor.TiposFormato;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MagicEasyDeckBuilderAPI.API.Controllers
{
    [Authorize]
    [Route("deck")]
    public class DeckController : ControllerBase
    {
        private readonly IDeckAppService _app;

        public DeckController(IDeckAppService app)
        {
            _app = app;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var idUser =  GetIdUsuario();

            var decks = await _app.ObterDecksPorUsuario(idUser);

            return Ok(decks);
        }

        [HttpGet, Route("{idDeck}")]
        public async Task<IActionResult> Get(string idDeck)
        {
            var idUser = GetIdUsuario();

            if (!Guid.TryParse(idDeck, out var id))
                return BadRequest("Id de deck inválido");

            var deck = await _app.ObterDeckPorId(id);

            if (deck == null)
                return NotFound();

            if (deck.IdUsuario != idUser)
                return Forbid();

            return Ok(deck);
        }

        [HttpPost]
        public async Task<IActionResult> IncluirDeck([FromBody]IncluiDeckViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.ValidationState);

            try
            {
                var idUsuario = GetIdUsuario();

                var idDeck = await _app.IncluirDeck(idUsuario, model.Nome, model.Formato);

                return Ok(new IncluirDeckRetornoViewModel  { IdDeck = idDeck });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost, Route("adicionar-carta")]
        public async Task<IActionResult> AdicionarCarta([FromBody]AdicionaCartaViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState.ValidationState);

                var idUsuario = GetIdUsuario();

                var deck = await _app.AdicionarCarta(idUsuario, model.IdDeck, model.IdScryFall, model.Tipo);

                return Ok(deck);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost, Route("alterar-capa")]
        public async Task<IActionResult> AlterarCapaDeck([FromBody]AlterarCapaViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.ValidationState);

            try
            {
                await _app.AlterarCapa(model.IdDeck, model.UrlImagem);

                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        private Guid GetIdUsuario()
        {
            var idUser = HttpContext.User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            return Guid.Parse(idUser);
        }
    }
}
