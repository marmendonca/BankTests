namespace TestQuestion1;

public class ContaBancaria
{
    public int NumeroConta { get; private set; }
    public string NomeTitular { get; private set; }
    public double Saldo { get; private set; }

    public ContaBancaria(int numeroConta, string nomeTitular, double saldo)
    {
        NumeroConta = numeroConta;
        NomeTitular = nomeTitular;
        Saldo = saldo;
    }

    public void SetName(string novoNome) => NomeTitular = novoNome;

    public void Deposito(double valor) => Saldo += valor;

    public void Sacar(double valor) => Saldo -= valor + 3.50;

    public override string ToString()
    {
        return $"Dados da conta: \nConta: {NumeroConta}, Titular: {NomeTitular}, Saldo: R$ {Saldo:F2}";
    }
}