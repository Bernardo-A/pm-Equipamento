namespace Equipamento.API.ViewModels
{
    public class BicicletaViewModel
    {   
        public int Id { get; set; } 
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

    public class BicicletaRetriveViewModel 
    {
        public string? IdTranca { get; set; }
        public string? IdBicicleta { get; set;}
        public string? IdFuncionario { get; set;}
    }
}
