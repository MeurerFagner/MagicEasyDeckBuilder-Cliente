using MagicEasyDeckBuilderAPI.Dominio.Entidades;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicEasyDeckBuilderAPI.Infra.MapEntidade
{
    public class UsuarioMap: MapBase<Usuario>
    {
        public override void Configure(EntityTypeBuilder<Usuario> builder)
        {
            base.Configure(builder);

            builder.Property(p => p.DataCadastro).ValueGeneratedOnAdd();

            builder.HasMany(h => h.Decks)
                .WithOne(w => w.Usuario)
                .HasForeignKey(f => f.IdUsuario);
        }
    }
}
