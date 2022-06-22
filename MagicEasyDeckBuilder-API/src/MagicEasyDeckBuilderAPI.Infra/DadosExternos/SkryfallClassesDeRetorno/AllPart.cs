namespace MagicEasyDeckBuilderAPI.Infra.DadosExternos.SkryfallClassesDeRetorno
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class AllPart
    {
        public string @object { get; set; }
        public string id { get; set; }
        public string component { get; set; }
        public string name { get; set; }
        public string type_line { get; set; }
        public string uri { get; set; }
    }


}
