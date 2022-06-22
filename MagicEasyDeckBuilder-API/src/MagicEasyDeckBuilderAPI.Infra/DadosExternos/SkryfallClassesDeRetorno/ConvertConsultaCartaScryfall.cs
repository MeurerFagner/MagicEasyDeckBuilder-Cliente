using MagicEasyDeckBuilderAPI.Core.Constantes;
using MagicEasyDeckBuilderAPI.Dominio.DTO;
using MagicEasyDeckBuilderAPI.Dominio.Entidades;
using MagicEasyDeckBuilderAPI.Dominio.ObjetoDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MagicEasyDeckBuilderAPI.Infra.DadosExternos.SkryfallClassesDeRetorno
{
    public static class ConvertConsultaCartaScryfall
    {
        private const decimal MAXIMO_CARTAS_POR_PAGINA = 175;

        public static ConsultaResponseDTO Map(Root responseSkryfall)
        {
            var responseDTO = new ConsultaResponseDTO
            {
                TemMaisPaginas = responseSkryfall.has_more,
                TotalDePaginas = (int)Math.Ceiling(responseSkryfall.total_cards / MAXIMO_CARTAS_POR_PAGINA),
                Cartas = responseSkryfall.data.Select(d => Map(d)).ToList()
            };

            return responseDTO;
        }

        public static Carta Map(CardData cartaData)
        {
            var carta = new Carta { 
                IdScryfall = cartaData.Id,
                Nome = cartaData.printed_name,
                NomeOriginal = cartaData.Name,
                Texto = cartaData.printed_text,
                TextoOriginal = cartaData.oracle_text,
                Tipo = cartaData.type_line,
                Layout = cartaData.layout,
                Raridade = cartaData.rarity,
                Cores = cartaData.colors,
                CardDuplo = cartaData.card_faces != null,
                IdentidadeDeCor = cartaData.color_identity,
                CustoMana = new CustoMana(cartaData.mana_cost),
                Keywords = cartaData.keywords,
                UrlImage = cartaData.image_uris?.normal,
                UrlCropImage = cartaData.image_uris?.art_crop,
                UrlApi = cartaData.Uri,
                Poder = cartaData.power,
                Resistencia = cartaData.toughness,
                Lealdade = cartaData.loyalty,
                LegalidadePorFormato = MapLegalidade(cartaData.legalities)
            };

            if (carta.CardDuplo)
            {
                var ladoUm = cartaData.card_faces[0];
                var ladoDois = cartaData.card_faces[1];

                carta.Faces.Add(new CartaFace
                {
                    Nome = ladoUm.printed_name,
                    NomeOriginal = ladoUm.name,
                    Texto = ladoUm.printed_text,
                    TextoOriginal = ladoUm.oracle_text,
                    Tipo = ladoUm.type_line,
                    Cores = ladoUm.colors, 
                    CustoMana = new CustoMana(ladoUm.mana_cost),
                    UrlImage = ladoUm?.image_uris?.normal,
                    UrlCropImage = ladoUm.image_uris?.art_crop,
                    Poder = ladoUm.power,
                    Resistencia = ladoUm.toughness,
                    Lealdade = ladoUm.loyalty
                });

                carta.Faces.Add(new CartaFace
                {
                    Nome = ladoDois.printed_name,
                    NomeOriginal = ladoDois.name,
                    Texto = ladoDois.printed_text,
                    TextoOriginal = ladoDois.oracle_text,
                    Tipo = ladoDois.type_line,
                    Cores = ladoDois.colors,
                    CustoMana = new CustoMana(ladoDois.mana_cost),
                    UrlImage = ladoDois?.image_uris?.normal,
                    UrlCropImage = ladoDois.image_uris?.art_crop,
                    Poder = ladoDois.power,
                    Resistencia = ladoDois.toughness,
                    Lealdade = ladoDois.loyalty
                });
            }
            
            return carta;
        }

        private static IDictionary<string,Legalidade> MapLegalidade(Legalities legalidade)
        {
            var listaLegalidade = new Dictionary<string, Legalidade>();

            listaLegalidade.Add(TiposFormatoJogo.BRAWL, Legalidade.Factory(legalidade.brawl));
            listaLegalidade.Add(TiposFormatoJogo.COMMANDER, Legalidade.Factory(legalidade.commander));
            listaLegalidade.Add(TiposFormatoJogo.LEGACY, Legalidade.Factory(legalidade.legacy));
            listaLegalidade.Add(TiposFormatoJogo.MODERN, Legalidade.Factory(legalidade.modern));
            listaLegalidade.Add(TiposFormatoJogo.PAUPER, Legalidade.Factory(legalidade.pauper));
            listaLegalidade.Add(TiposFormatoJogo.PIONEER, Legalidade.Factory(legalidade.pioneer));
            listaLegalidade.Add(TiposFormatoJogo.VINTAGE, Legalidade.Factory(legalidade.vintage));

            return listaLegalidade;
        }

        public static EdicaoDTO Map(SetData set)
        {
            return new EdicaoDTO
            {
                Code = set.code,
                Nome = set.name,
                IconUriSvg = set.icon_svg_uri
            };
        }
    }

}
