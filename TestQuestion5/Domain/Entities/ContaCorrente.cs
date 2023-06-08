namespace TestQuestion5.Domain.Entities;

public class ContaCorrente
{
    public string IdContaCorrente { get; set; }
    public int Numero { get; set; }
    public string Nome { get; set; }
    public bool Ativo { get; set; }

    public ContaCorrente(string idContaCorrente, int numero, string nome, bool ativo)
    {
        IdContaCorrente = idContaCorrente;
        Numero = numero;
        Nome = nome;
        Ativo = ativo;
    }
}