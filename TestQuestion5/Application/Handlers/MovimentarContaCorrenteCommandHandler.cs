using MediatR;
using TestQuestion5.Application.Commands;
using TestQuestion5.Domain.Entities;
using TestQuestion5.Domain.Exceptions;
using TestQuestion5.Domain.Interfaces.Repositories;

namespace TestQuestion5.Application.Handlers;

public class MovimentarContaCorrenteCommandHandler : IRequestHandler<MovimentarContaCorrenteCommand, string>
{
    private readonly IContaCorrenteRepository _contaCorrenteRepository;
    private readonly IMovimentoRepository _movimentoRepository;
    private readonly IIdempotenciaRepository _idempotenciaRepository;

    public MovimentarContaCorrenteCommandHandler(
        IContaCorrenteRepository contaCorrenteRepository,
        IMovimentoRepository movimentoRepository,
        IIdempotenciaRepository idempotenciaRepository)
    {
        _contaCorrenteRepository = contaCorrenteRepository;
        _movimentoRepository = movimentoRepository;
        _idempotenciaRepository = idempotenciaRepository;
    }

    public async Task<string> Handle(MovimentarContaCorrenteCommand command, CancellationToken cancellationToken)
    {
        var contaCorrente = await _contaCorrenteRepository.GetByIdAsync(command.IdContaCorrente);

        ValidarContaCorrente(contaCorrente, command.Valor);

        var movimentacao = await InserirMovimento(contaCorrente.IdContaCorrente, command);

        await InserirIdempotenciaAsync(command.IdRequisicao);

        return movimentacao.IdMovimento;
    }

    private void ValidarContaCorrente(ContaCorrente contaCorrente, decimal valorMovimento)
    {
        if (contaCorrente is null)
            throw new DomainException("Conta corrente não encontrada", "INVALID_ACCOUNT");

        if (!contaCorrente.Ativo)
            throw new DomainException("Conta corrente inativa, não é possível fazer movimentações.", "INACTIVE_ACCOUNT");

        if (valorMovimento < 0)
            throw new DomainException("Conta corrente com valor negativo, não é possível fazer movimentações.", "INVALID_VALUE");
    }

    private async Task<Movimento> InserirMovimento(string idContaCorrente, MovimentarContaCorrenteCommand command)
    {
        var movimento = new Movimento(
            Guid.NewGuid().ToString(),
            idContaCorrente,
            DateTime.Now,
            command.TipoMovimento.ToString(),
            command.Valor);

        await _movimentoRepository.InserirMovimentoAsync(movimento);

        return movimento;
    }

    private async Task InserirIdempotenciaAsync(string idRequisicao)
    {
        var idempotencia = new Idempotencia(Guid.NewGuid().ToString(), idRequisicao, "Movimentação feita com sucesso");

        await _idempotenciaRepository.InserirIdempotenciaAsync(idempotencia);
    }
}