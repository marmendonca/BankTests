using TestQuestion5.Domain.Entities;

namespace TestQuestion5.Domain.Interfaces.Repositories;

public interface IContaCorrenteRepository
{
    Task<IEnumerable<ContaCorrente>> GetAllAsync();

    Task<ContaCorrente> GetByIdAsync(string idcontacorrente);

    Task<int> InsertAsync(ContaCorrente entity);

    Task<bool> UpdateAsync(ContaCorrente entity);

    Task<bool> DeleteAsync(int id);
}