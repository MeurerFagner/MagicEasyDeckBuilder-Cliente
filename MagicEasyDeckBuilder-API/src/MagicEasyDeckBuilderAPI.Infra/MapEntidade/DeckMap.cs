using MagicEasyDeckBuilderAPI.Dominio.Entidades;
using MagicEasyDeckBuilderAPI.Dominio.ObjetoDeValor.TiposFormato;
using MagicEasyDeckBuilderAPI.Infra.MapEntidade.Converters;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicEasyDeckBuilderAPI.Infra.MapEntidade
{
    public class DeckMap : MapBase<Deck>
    {
        public override void Configure(EntityTypeBuilder<Deck> builder)
        {
            base.Configure(builder);

            builder.Property(p => p.TipoFormato)
                .HasConversion(c => c.Nome,
                               c => TipoFormatoBase.Factory(c));

            builder.Ignore(i => i.MainDeck);
            builder.Ignore(i => i.SideDeck);
            builder.Ignore(i => i.MaybeDeck);

            builder.Property(p => p.Erros).HasConversion(
                c => string.Join(";", c),
                c => c.Split(";", StringSplitOptions.None),
                ComparerHelper.CompareStrings());

            builder.HasOne(h => h.Usuario)
                .WithMany(w => w.Decks)
                .HasForeignKey(f => f.IdUsuario);


        }
    }
}
