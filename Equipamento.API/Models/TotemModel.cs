namespace Equipamento.API.Models
{
    public class TotemModel : TotemDTO
    {
        public int Id { get; set; }
        public List<TrancaModel>? Trancas { get; set; }
    }
    public class TotemDTO
    {
        public string? Localizacao { get; set; }
        public string? Descricao { get; set; }
    }
}
