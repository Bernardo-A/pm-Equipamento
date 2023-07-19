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

    public class FuncionarioModel
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? DataNascimento { get; set; }
        public string? CPF { get; set; }
        public string? Email { get; set; }
        public string? Senha { get; set; }
        public string? Funcao { get; set; }
        public bool Habilitado { get; set; } = true;
    }

    public class EmailDto
    {
        public string? Email { get; set; }
        public string? Assunto { get; set; }
        public string? Mensagem { get; set; }
    }
}
