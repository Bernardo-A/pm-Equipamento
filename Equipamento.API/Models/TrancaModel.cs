namespace Equipamento.API.Models
{
    public class TrancaModel : TrancaDto
    {
        public int Id { get; set; }
        public BicicletaModel? Bicicleta { get; set; }
    }
    public class TrancaDto
    {
        public int? Numero { get; set; }
        public string? Localizacao { get; set; }
        public string? AnoDeFabricacao { get; set; }
        public string? Modelo { get; set; }
        public string? Status { get; set; }
    }
}
