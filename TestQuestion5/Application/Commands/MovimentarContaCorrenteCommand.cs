using MediatR;
using TestQuestion5.Domain.Enumerators;

namespace TestQuestion5.Application.Commands;

public class MovimentarContaCorrenteCommand : IRequest<string>
{
    public string IdRequisicao { get; set; }
    public string IdContaCorrente { get; set; }
    public decimal Valor { get; set; }
    /// <summary>
    /// TipoMovimento 1 = Crédito
    /// TipoMovimento 2 = Débito
    /// </summary>
    public TipoMovimento TipoMovimento { get; set; }
}