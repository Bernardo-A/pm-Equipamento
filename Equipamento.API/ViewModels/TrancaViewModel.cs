namespace Equipamento.API.ViewModels
{
    public class TrancaViewModel : TrancaInsertViewModel
    {
        public int Id { get; set; }
        public BicicletaViewModel? Bicicleta { get; set; }
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
