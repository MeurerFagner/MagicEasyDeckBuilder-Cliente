using MagicEasyDeckBuilderAPI.Core.Data;
using MagicEasyDeckBuilderAPI.Dominio.Entidades;
using MagicEasyDeckBuilderAPI.Dominio.Interfaces.Infra;
using MagicEasyDeckBuilderAPI.Dominio.ObjetoDeValor.TiposFormato;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicEasyDeckBuilderAPI.Infra.Repositorio
{
    public class DeckRepository : BaseRepository, IDeckRepository
    {
        public DeckRepository(Context context) : base(context)
        {
        }

        public async Task IncluirDeck(Deck deck)
        {
            _context.Add(deck);

            await UnitOfWork.Commit();
        }

        public async Task<IEnumerable<Deck>> ObterDeckPorUsuario(Guid idUsuario)
        {
            return await _context.Decks.Include(i => i.Cartas).ThenInclude(c => c.Carta).Where(d => d.IdUsuario == idUsuario).ToListAsync();
        }

        public async Task<Deck> ObterDeck(Guid id)
        {
            var deck = await _context.Decks
                .Include(i => i.Cartas)
                .ThenInclude(c => c.Carta)
                .ThenInclude(f => f.Faces)
                .FirstOrDefaultAsync(d => d.Id == id);

            return deck;
        }

        public async Task<Carta> ObterCartaPorIdScryfall(string idCarta)
        {
            var carta = await _context.Cartas.Include(i =>i.Faces).FirstOrDefaultAsync(c => c.IdScryfall == idCarta);

            return carta;
        }

        public async Task<Carta> ObterCartaPorIdCarta(Guid idCarta)
        {
            var carta = await _context.Cartas.Include(i => i.Faces).FirstOrDefaultAsync(c => c.Id == idCarta);

            return carta;
        }

        public async Task MudarCapaDeck(Guid idDeck, string uriCapaDeck)
        {
            var deck = await ObterDeck(idDeck);

            deck.Capa = uriCapaDeck;

            _context.Update(deck);

            await UnitOfWork.Commit();
        }

        public async Task SalvarDeck(Deck deck)
        {
            _context.Update(deck);

            await UnitOfWork.Commit();
        }

    }
}
