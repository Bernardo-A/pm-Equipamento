namespace Equipamento.API.Models
{
    public class RedeDto 
    {
        public int TrancaId { get; set; }
        public int FuncionarioId { get; set;}
    }
    
    public class RedeRemoveDto : RedeDto
    {
        public int StatusAcaoReparador { get; set; }
    }

    public class TrancaRedeDto : RedeDto 
    {
        public int TotemId { get; set; }
    }

    public class BicicletaRedeDto : RedeDto
    {
        public int BicicletaId { get; set; }
    }
    
    public class BicicletaRemoveDto : BicicletaRedeDto 
    {
        public string? StatusAcaoReparador { get; set; }
    }
}
