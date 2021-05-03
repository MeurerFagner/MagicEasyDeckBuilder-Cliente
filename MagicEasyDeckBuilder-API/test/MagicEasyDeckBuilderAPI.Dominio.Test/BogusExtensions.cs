using Bogus;
using MagicEasyDeckBuilderAPI.Core.Constantes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MagicEasyDeckBuilderAPI.Dominio.Test
{
    public static class BogusExtensions
    {
        public static string FormatoJogo(this Faker faker)
        {
            return faker.Random.ArrayElement(
                new string[]
                {
                    TiposFormatoJogo.CASUAL,
                    TiposFormatoJogo.BRAWL,
                    TiposFormatoJogo.BRAWL_LIVRE,
                    TiposFormatoJogo.COMMANDER,
                    TiposFormatoJogo.COMMANDER_LIVRE,
                    TiposFormatoJogo.LEGACY,
                    TiposFormatoJogo.MODERN,
                    TiposFormatoJogo.PAUPER,
                    TiposFormatoJogo.PIONEER,
                    TiposFormatoJogo.VINTAGE
                }
            );
        }
    }
}
