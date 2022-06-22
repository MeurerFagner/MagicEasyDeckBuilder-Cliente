using MagicEasyDeckBuilderAPI.Dominio.Entidades;
using MagicEasyDeckBuilderAPI.Infra.MapEntidade.Converters;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicEasyDeckBuilderAPI.Infra.MapEntidade
{
    public class CartaDeckMap : MapBase<CartaDeck>
    {
        public override void Configure(EntityTypeBuilder<CartaDeck> builder)
        {
            base.Configure(builder);

            builder.Property(p => p.Erros).HasConversion(
                c => string.Join(";", c),
                c => c.Split(";", StringSplitOptions.None),
                ComparerHelper.CompareStrings());

            builder.Property(p => p.TipoDeck).HasConversion<string>();

            builder.HasOne(h => h.Carta)
                .WithMany(w => w.DeckCartas)
                .HasForeignKey(f => f.IdCarta);

            builder.HasOne(h => h.Deck)
                .WithMany(w => w.Cartas)
                .HasForeignKey(f => f.IdDeck);
        }
    }
}
