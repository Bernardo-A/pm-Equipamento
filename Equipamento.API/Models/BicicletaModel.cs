namespace Equipamento.API.Models
{
    public class BicicletaModel : BicicletaDTO
    {   
        public int Id { get; set; } 
    }
    public class BicicletaDTO
    {
        public string? Marca { get; set; }
        public string? Modelo { get; set; }
        public string? Ano { get; set; }
        public string? Numero { get; set; }
        public string? Status { get; set; }
    }
}
