namespace TestQuestion5.Domain.Entities;

public class Idempotencia
{
    public string Chave_Idempotencia { get; set; }
    public string Requisicao { get; set; }
    public string Resultado { get; set; }

    public Idempotencia(string chave_Idempotencia, string requisicao, string resultado)
    {
        Chave_Idempotencia = chave_Idempotencia;
        Requisicao = requisicao;
        Resultado = resultado;
    }
}