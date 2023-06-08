using MediatR;
using System.Globalization;
using TestQuestion5.Application.Queries;
using TestQuestion5.Domain.Dtos;
using TestQuestion5.Domain.Entities;
using TestQuestion5.Domain.Exceptions;
using TestQuestion5.Domain.Interfaces.Repositories;

namespace TestQuestion5.Application.Handlers;

public class ConsultarSaldoContaCorrenteQueryHandler : IRequestHandler<ConsultarSaldoContaCorrenteQuery, ConsultaSaldoContaCorrenteDto>
{
    private readonly IMovimentoRepository _movimentoRepository;
    private readonly IContaCorrenteRepository _contaCorrenteRepository;

    public ConsultarSaldoContaCorrenteQueryHandler(IMovimentoRepository movimentoRepository, IContaCorrenteRepository contaCorrenteRepository)
    {
        _movimentoRepository = movimentoRepository;
        _contaCorrenteRepository = contaCorrenteRepository;
    }

    public async Task<ConsultaSaldoContaCorrenteDto> Handle(ConsultarSaldoContaCorrenteQuery query, CancellationToken cancellationToken)
    {
        var contaCorrente = await _contaCorrenteRepository.GetByIdAsync(query.IdContaCorrente);

        ValidarContaCorrente(contaCorrente);

        var saldo = await _movimentoRepository.GetSaldoAtualAsync(query.IdContaCorrente);

        return new ConsultaSaldoContaCorrenteDto(contaCorrente.Numero, contaCorrente.Nome, saldo.ToString("C", CultureInfo.CurrentCulture));
    }

    private void ValidarContaCorrente(ContaCorrente contaCorrente)
    {
        if (contaCorrente is null)
            throw new DomainException("Conta corrente não encontrada", "INVALID_ACCOUNT");

        if (!contaCorrente.Ativo)
            throw new DomainException("Conta corrente inativa, não é possível fazer movimentações.", "INACTIVE_ACCOUNT");
    }
}