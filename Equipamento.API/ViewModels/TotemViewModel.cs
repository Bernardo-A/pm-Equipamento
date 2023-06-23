namespace Equipamento.API.ViewModels
{
    public class TotemViewModel
    {
        public int Id { get; set; } = 0;
        public string? Localizacao { get; set; }
        public string? Descricao { get; set; }
        public string? Status { get; set;}
    }
    public class TotemInsertViewModel
    {
        public string? Localizacao { get; set; }
        public string? Descricao { get; set; }
    }
}
