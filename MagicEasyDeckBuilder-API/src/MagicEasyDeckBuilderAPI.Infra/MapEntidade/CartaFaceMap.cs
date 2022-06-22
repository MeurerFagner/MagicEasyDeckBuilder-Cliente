using MagicEasyDeckBuilderAPI.Dominio.Entidades;
using MagicEasyDeckBuilderAPI.Dominio.ObjetoDeValor;
using MagicEasyDeckBuilderAPI.Infra.MapEntidade.Converters;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MagicEasyDeckBuilderAPI.Infra.MapEntidade
{
    public class CartaFaceMap: MapBase<CartaFace>
    {
        public override void Configure(EntityTypeBuilder<CartaFace> builder)
        {
            base.Configure(builder);
            builder.Property(p => p.Cores).HasConversion<ListStringConverter>(ComparerHelper.CompareStrings());
            builder.Property(p => p.CustoMana).HasConversion(
               c => c.Custo,
               c => new CustoMana(c));

            builder.HasOne(h => h.Carta)
                .WithMany(w => w.Faces)
                .HasForeignKey(f => f.IdCarta);

        }

    }
}
