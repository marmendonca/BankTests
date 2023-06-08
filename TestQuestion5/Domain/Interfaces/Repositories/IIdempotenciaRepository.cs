using TestQuestion5.Domain.Entities;

namespace TestQuestion5.Domain.Interfaces.Repositories;

public interface IIdempotenciaRepository
{
    Task<int> InserirIdempotenciaAsync(Idempotencia idempotencia);
}