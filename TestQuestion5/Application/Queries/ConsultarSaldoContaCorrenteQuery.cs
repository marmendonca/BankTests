using MediatR;
using TestQuestion5.Domain.Dtos;

namespace TestQuestion5.Application.Queries;

public class ConsultarSaldoContaCorrenteQuery : IRequest<ConsultaSaldoContaCorrenteDto>
{
    public string IdContaCorrente { get; set; }
}