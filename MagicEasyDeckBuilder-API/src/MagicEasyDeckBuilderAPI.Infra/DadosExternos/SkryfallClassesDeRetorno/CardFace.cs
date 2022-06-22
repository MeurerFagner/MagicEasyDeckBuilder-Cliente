using System.Collections.Generic;

namespace MagicEasyDeckBuilderAPI.Infra.DadosExternos.SkryfallClassesDeRetorno
{
    public class CardFace
    {
        public string @object { get; set; }
        public string name { get; set; }
        public string printed_name { get; set; }
        public string mana_cost { get; set; }
        public string type_line { get; set; }
        public string printed_type_line { get; set; }
        public string oracle_text { get; set; }
        public string printed_text { get; set; }
        public List<string> colors { get; set; }
        public string power { get; set; }
        public string toughness { get; set; }
        public string artist { get; set; }
        public string artist_id { get; set; }
        public string illustration_id { get; set; }
        public ImageUris image_uris { get; set; }
        public string flavor_name { get; set; }
        public List<string> color_indicator { get; set; }
        public string loyalty { get; set; }
    }


}
