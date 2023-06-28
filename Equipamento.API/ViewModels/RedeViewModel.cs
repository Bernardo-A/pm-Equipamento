namespace Equipamento.API.ViewModels
{
    public class RedeAddViewModel 
    {
        public int TrancaId { get; set; }
        public int FuncionarioId { get; set;}
    }
    
    public class RedeRemoveViewModel : RedeAddViewModel
    {
        public int StatusAcaoReparador { get; set; }
    }

    public class TrancaRedeViewModel : RedeAddViewModel 
    {
        public int TotemId { get; set; }
    }

    public class BicicletaRedeAddViewModel : RedeAddViewModel
    {
        public int BicicletaId { get; set; }
    }
      
}
