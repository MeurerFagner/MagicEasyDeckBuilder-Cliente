using AutoMapper;
using MagicEasyDeckBuilderAPI.Core.Constantes;
using MagicEasyDeckBuilderAPI.Core.ViewModel;
using MagicEasyDeckBuilderAPI.Dominio.Entidades;
using MagicEasyDeckBuilderAPI.Dominio.Interfaces.Infra;
using MagicEasyDeckBuilderAPI.Dominio.ObjetoDeValor.TiposFormato;

namespace MagicEasyDeckBuilderAPI.App.Services
{
    public class DeckAppService: IDeckAppService
    {
        private readonly IDeckRepository _deckRepository;
        private readonly IScryfallApiService _scryfallApiService;
        private readonly IMapper _mapper;

        public DeckAppService(IDeckRepository deckRepository, IScryfallApiService scryfallApiService, IMapper mapper)
        {
            _deckRepository = deckRepository;
            _scryfallApiService = scryfallApiService;
            _mapper = mapper;
        }

        public async Task<DeckViewModel> AdicionarCarta(Guid idUsuario, Guid idDeck, string idScryFall, string tipo)
        {
            var deck = await _deckRepository.ObterDeck(idDeck);

            if (deck == null)
                return null;

            if (deck.IdUsuario != idUsuario)
                throw new Exception("Deck não possui a um usário diferente");

            var carta = await _deckRepository.ObterCartaPorIdScryfall(idScryFall);

            if (carta == null)
            {
                carta = await _scryfallApiService.BucaCartaPorIdScryFall(idScryFall);

            }

            AdicionarCarta(tipo, deck, carta);

            await _deckRepository.SalvarDeck(deck);

            var deckViewModel = _mapper.Map<DeckViewModel>(deck);

            return deckViewModel;
        }

        private void AdicionarCarta(string tipo, Deck deck, Carta? carta)
        {
            if (tipo == TipoInclusaoCarta.MAIN)
                deck.AdicionarCartaMainDeck(carta);
            else if (tipo == TipoInclusaoCarta.SIDE)
                deck.AdicionarCartaSideDeck(carta);
            else if (tipo == TipoInclusaoCarta.MAYBE)
                deck.AdicionarCartaMaybeDeck(carta);
            else if (tipo == TipoInclusaoCarta.COMANDANTE)
                deck.AdicionarComandante(carta);
            else
                throw new Exception("Tipo de deck não identificado");
        }

        public async Task AlterarCapa(Guid idDeck, string urlImagem)
        {
            var deck = await _deckRepository.ObterDeck(idDeck);

            deck.Capa = urlImagem;

            await _deckRepository.SalvarDeck(deck);
        }

        public async Task<Guid> IncluirDeck(Guid idUsuario, string nome, string formato)
        {
            var deck = new Deck
            {
                IdUsuario = idUsuario,
                Nome = nome,
                TipoFormato = TipoFormatoBase.Factory(formato)
            };

            await _deckRepository.IncluirDeck(deck);

            return deck.Id;
        }

        public async Task<Deck> ObterDeckPorId(Guid idDeck)
        {
            return await _deckRepository.ObterDeck(idDeck);
        }

        public async Task<IEnumerable<DeckViewModel>> ObterDecksPorUsuario(Guid idUser)
        {
            var decks = await _deckRepository.ObterDeckPorUsuario(idUser);

            return decks.Select(d => new DeckViewModel(
                d.Id, 
                d.Nome, 
                d.TipoFormato.Nome, 
                d.Cartas.SelectMany(c => c.Carta.IdentidadeDeCor).Distinct(), 
                d.Capa));
            
        }

        public async Task<DeckViewModel> MoverCarta(Guid idDeck, Guid idCarta, string tipoDeckOrigem, string tipodeckDestino, Guid idUsuario)
        {
            var deck = await _deckRepository.ObterDeck(idDeck);

            if (deck == null)
                return null;

            if (deck.IdUsuario != idUsuario)
                throw new Exception("Deck não pertence a um usuário diferente");

            var carta = await _deckRepository.ObterCartaPorIdCarta(idCarta);

            RemoverCarta(tipoDeckOrigem, deck, carta);

            AdicionarCarta(tipodeckDestino, deck, carta);

            await _deckRepository.SalvarDeck(deck);

            var deckViewModel = _mapper.Map<DeckViewModel>(deck);

            return deckViewModel;
        }

        public async  Task<DeckViewModel> RemoverCarta(Guid idDeck, Guid idCarta, string tipo, Guid idUsuario)
        {
            var deck = await _deckRepository.ObterDeck(idDeck);

            if (deck == null)
                return null;

            if (deck.IdUsuario != idUsuario)
                throw new Exception("Deck não possui a um usário diferente");

            var carta = await _deckRepository.ObterCartaPorIdCarta(idCarta);

            RemoverCarta(tipo, deck, carta);

            await _deckRepository.SalvarDeck(deck);

            var deckViewModel = _mapper.Map<DeckViewModel>(deck);

            return deckViewModel;
        }

        private void RemoverCarta(string tipo, Deck deck, Carta carta)
        {
            if (tipo == TipoInclusaoCarta.MAIN)
                deck.RemoverCartaMainDeck(carta);
            else if (tipo == TipoInclusaoCarta.SIDE)
                deck.RemoverCartaSideDeck(carta);
            else if (tipo == TipoInclusaoCarta.MAYBE)
                deck.RemoverCartaMaybeDeck(carta);
        }
    }
}
