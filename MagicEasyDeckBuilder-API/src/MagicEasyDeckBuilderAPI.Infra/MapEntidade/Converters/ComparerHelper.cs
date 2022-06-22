using MagicEasyDeckBuilderAPI.Core.Constantes;
using MagicEasyDeckBuilderAPI.Dominio.ObjetoDeValor;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicEasyDeckBuilderAPI.Infra.MapEntidade.Converters
{
    public class ComparerHelper
    {
        public static ValueComparer<IEnumerable<Cor>> CompareCores()
        {
            var compare = new ValueComparer<IEnumerable<Cor>>(
                (c1, c2) => c1.SequenceEqual(c2),
                c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                c => c.ToList());

            return compare;
        }

        public static ValueComparer<IEnumerable<string>> CompareStrings()
        {
            var compare = new ValueComparer<IEnumerable<string>>(
                (c1, c2) => c1.SequenceEqual(c2),
                c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                c => c.ToList());

            return compare;
        }
    }
}
