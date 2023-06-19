namespace Equipamento.API.ViewModels
{
    public class BicicletaViewModel
    {
        public int Id { get; set; } = 0;
        public string? Marca { get; set; }
        public string? Modelo { get; set; }
        public string? Numero { get; set; }
        public string? Ano { get; set;}
        public string? Status { get; set;}

    }
    public class BicicletaInsertViewModel
    {
        public string? Marca { get; set; }
        public string? Modelo { get; set; }
        public string? Ano { get; set; }
        public string? Numero { get; set; }
        public string? Status { get; set; }
    }
}
