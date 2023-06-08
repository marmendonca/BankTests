using Dapper;
using System.Data;
using TestQuestion5.Domain.Entities;
using TestQuestion5.Domain.Interfaces.Repositories;

namespace TestQuestion5.Infrastructure.Repositories;

public class ContaCorrenteRepository : IContaCorrenteRepository
{
    private readonly IDbConnection _dbConnection;

    public ContaCorrenteRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<IEnumerable<ContaCorrente>> GetAllAsync()
    {
        string sql = "SELECT * FROM ContaCorrente";
        return await _dbConnection.QueryAsync<ContaCorrente>(sql);
    }

    public async Task<ContaCorrente> GetByIdAsync(string idcontacorrente)
    {
        string sql = "SELECT * FROM ContaCorrente WHERE idcontacorrente = @Id";
        return await _dbConnection.QueryFirstOrDefaultAsync<ContaCorrente>(sql, new { Id = idcontacorrente });
    }

    public async Task<int> InsertAsync(ContaCorrente entity)
    {
        string sql = "INSERT INTO ContaCorrente (Column1, Column2, ...) VALUES (@Column1, @Column2, ...)";
        return await _dbConnection.ExecuteAsync(sql, entity);
    }

    public async Task<bool> UpdateAsync(ContaCorrente entity)
    {
        string sql = "UPDATE ContaCorrente SET Column1 = @Column1, Column2 = @Column2, ... WHERE idcontacorrente = @idcontacorrente";
        int rowsAffected = await _dbConnection.ExecuteAsync(sql, entity);
        return rowsAffected > 0;
    }

    public async Task<bool> DeleteAsync(int idcontacorrente)
    {
        string sql = "DELETE FROM ContaCorrente WHERE Id = @Id";
        int rowsAffected = await _dbConnection.ExecuteAsync(sql, new { Id = idcontacorrente });
        return rowsAffected > 0;
    }
}