namespace TestQuestion5.Domain.Dtos;

public class ConsultaSaldoContaCorrenteDto
{
    public int NumeroConta { get; set; }
    public string NomeTitular { get; set; }
    public DateTime DataConsulta { get; set; } = DateTime.Now;
    public string SaldoAtual { get; set; }

    public ConsultaSaldoContaCorrenteDto(int numeroConta, string nomeTitular, string saldoAtual)
    {
        NumeroConta = numeroConta;
        NomeTitular = nomeTitular;
        SaldoAtual = saldoAtual;
    }
}