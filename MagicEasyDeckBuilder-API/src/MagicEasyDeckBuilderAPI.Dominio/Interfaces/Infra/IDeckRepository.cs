using MagicEasyDeckBuilderAPI.Core.Data;
using MagicEasyDeckBuilderAPI.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicEasyDeckBuilderAPI.Dominio.Interfaces.Infra
{
    public interface IDeckRepository : IRepository
    {
        Task IncluirDeck(Deck deck);
        Task MudarCapaDeck(Guid idDeck, string uriCapaDeck);
        Task<Carta> ObterCartaPorIdCarta(Guid idCarta);
        Task<Carta> ObterCartaPorIdScryfall(string idCarta);
        Task<Deck> ObterDeck(Guid id);
        Task<IEnumerable<Deck>> ObterDeckPorUsuario(Guid idUsuario);
        Task SalvarDeck(Deck deck);
    }
}
