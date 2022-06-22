using MagicEasyDeckBuilderAPI.Dominio.Entidades;
using MagicEasyDeckBuilderAPI.Dominio.ObjetoDeValor;
using MagicEasyDeckBuilderAPI.Infra.MapEntidade.Converters;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MagicEasyDeckBuilderAPI.Infra.MapEntidade
{
    public class CartaMap : MapBase<Carta>
    {
        public override void Configure(EntityTypeBuilder<Carta> builder)
        {
            base.Configure(builder);
            builder.Property(p => p.LegalidadePorFormato).HasConversion<LegalidadeFormatoConverter>();
            builder.Property(p => p.Keywords).HasConversion<ListStringConverter>(ComparerHelper.CompareStrings());
            builder.Property(p => p.Cores).HasConversion<ListStringConverter>(ComparerHelper.CompareStrings());
            builder.Property(p => p.IdentidadeDeCor).HasConversion<ListStringConverter>(ComparerHelper.CompareStrings());

            builder.Property(p => p.CustoMana).HasConversion(
                c => c.Custo,
                c => new CustoMana(c));
           
        }
    }
}
