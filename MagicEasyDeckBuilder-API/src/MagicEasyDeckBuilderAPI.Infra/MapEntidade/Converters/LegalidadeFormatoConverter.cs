using MagicEasyDeckBuilderAPI.Core.Constantes;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicEasyDeckBuilderAPI.Infra.MapEntidade.Converters
{
    public class LegalidadeFormatoConverter: ValueConverter<IDictionary<string,Legalidade>,string>
    {
        public LegalidadeFormatoConverter():
            base(l => LegalidadeFormatosParaString(l),
                 l => StringParaLegalidadeFormats(l))
        { }

        protected static IDictionary<string, Legalidade> StringParaLegalidadeFormats(string legalidadelist)
        {
            var legalidadeFormato = JsonConvert.DeserializeObject<IDictionary<string, string>>(legalidadelist);

            var retorno = new Dictionary<string, Legalidade>();

            foreach (var item in legalidadeFormato)
            {
                retorno.Add(item.Key, Legalidade.Factory(item.Value));
            }

            return retorno;
        }

        protected static string LegalidadeFormatosParaString(IDictionary<string,Legalidade> legalidadeList)
        {
            var legalidadeDict = new Dictionary<string, string>();

            foreach (var item in legalidadeList)
            {
                legalidadeDict.Add(item.Key, item.Value.ToString());
            }

            return JsonConvert.SerializeObject(legalidadeDict);
        }
    }

}
