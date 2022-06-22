using MagicEasyDeckBuilderAPI.Dominio.ObjetoDeValor;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MagicEasyDeckBuilderAPI.Infra.MapEntidade.Converters
{
    public class ListStringConverter : ValueConverter<IEnumerable<string>, string>
    {
        public ListStringConverter() :
            base(c => CoresParaString(c),
                 c => StringParaCores(c))
        {
        }

        public static string CoresParaString(IEnumerable<string> cores)
        {
            return string.Join(",", cores);
        }

        public static IEnumerable<string> StringParaCores(string cores)
        {
            if (string.IsNullOrEmpty(cores))
                return null;

            var splitColors = cores.Split(",");

            return splitColors;
        }
    }
}
