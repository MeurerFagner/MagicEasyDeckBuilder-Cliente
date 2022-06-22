namespace MagicEasyDeckBuilderAPI.App.ViewModel
{
    public class CartaFaceViewModel
    {
        public string Nome { get; set; }
        public string NomeOriginal { get; set; }
        public string Texto { get; set; }
        public string TextoOriginal { get; set; }
        public string Tipo { get; set; }
        public IEnumerable<string> Cores { get; set; }
        public string UrlImage { get; set; }
        public string UrlCropImage { get; set; }
        public string Poder { get; set; }
        public string Resistencia { get; set; }
        public string Lealdade { get; set; }
        public string CustoMana { get; set; }
    }
}