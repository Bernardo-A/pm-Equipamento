namespace Equipamento.API.Models
{
    public class TotemModel : TotemDto
    {
        public int Id { get; set; }
        public List<TrancaModel>? Trancas { get; set; }
    }
    public class TotemDto
    {
        public string? Localizacao { get; set; }
        public string? Descricao { get; set; }
    }
}
