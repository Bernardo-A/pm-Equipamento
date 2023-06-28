namespace Equipamento.API.ViewModels
{
    public class TotemViewModel : TotemInsertViewModel
    {
        public int Id { get; set; }
        public List<TrancaViewModel>? Trancas { get; set; }
    }
    public class TotemInsertViewModel
    {
        public string? Localizacao { get; set; }
        public string? Descricao { get; set; }
    }
}
