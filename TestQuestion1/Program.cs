// See https://aka.ms/new-console-template for more information
using TestQuestion1;

Console.Write("Entre o número da conta: ");
var numeroConta = int.Parse(Console.ReadLine());

Console.Write("Entre o titular da conta: ");
string nomeTitular = Console.ReadLine();

Console.Write("Haverá depósito inicial (s/n)? ");
var existeDepositoInicial = Console.ReadLine();

double depositoInicial = 0;
if (existeDepositoInicial.ToLower() is "s")
{
    Console.Write("Entre o valor de depósito inicial: ");
    depositoInicial = double.Parse(Console.ReadLine());
}

var contaBancaria = new ContaBancaria(numeroConta, nomeTitular, depositoInicial);
Console.WriteLine(contaBancaria.ToString());


Console.Write("Entre um valor para depósito: ");
var valorDeposito = double.Parse(Console.ReadLine());
contaBancaria.Deposito(valorDeposito);
Console.WriteLine(contaBancaria.ToString());

Console.Write("Entre um valor para saque: ");
var valorSaque = double.Parse(Console.ReadLine());
contaBancaria.Sacar(valorSaque);
Console.WriteLine(contaBancaria.ToString());