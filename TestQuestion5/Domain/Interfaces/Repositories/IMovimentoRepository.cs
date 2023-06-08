using TestQuestion5.Domain.Entities;

namespace TestQuestion5.Domain.Interfaces.Repositories;

public interface IMovimentoRepository
{
    Task<int> InserirMovimentoAsync(Movimento movimento);

    Task<decimal> GetSaldoAtualAsync(string idContaCorrente);
}