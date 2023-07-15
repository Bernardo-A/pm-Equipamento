namespace Equipamento.API.Models
{
    public class RedeDTO 
    {
        public int TrancaId { get; set; }
        public int FuncionarioId { get; set;}
    }
    
    public class RedeRemoveDTO : RedeDTO
    {
        public int StatusAcaoReparador { get; set; }
    }

    public class TrancaRedeDTO : RedeDTO 
    {
        public int TotemId { get; set; }
    }

    public class BicicletaRedeDTO : RedeDTO
    {
        public int BicicletaId { get; set; }
    }
    
    public class BicicletaRemoveDTO : BicicletaRedeDTO 
    {
        public string? StatusAcaoReparador { get; set; }
    }
}
