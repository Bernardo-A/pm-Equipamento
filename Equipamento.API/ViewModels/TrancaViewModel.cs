namespace Equipamento.API.ViewModels
{
    public class TrancaViewModel
    {
        public int Id { get; set; }
        public BicicletaViewModel? Bicicleta { get; set; }
        public int? Numero { get; set; }
        public string? Localizacao { get; set; }
        public string? AnoDeFabricacao { get; set; }
        public string? Modelo { get; set; }
        public string? Status { get; set; }
    }
    public class TrancaInsertViewModel
    {
        public int? Numero { get; set; }
        public string? Localizacao { get; set; }
        public string? AnoDeFabricacao { get; set; }
        public string? Modelo { get; set; }
        public string? Status { get; set; }
    }
}
