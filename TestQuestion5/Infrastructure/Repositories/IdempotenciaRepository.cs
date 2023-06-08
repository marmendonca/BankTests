using Dapper;
using System.Data;
using TestQuestion5.Domain.Entities;
using TestQuestion5.Domain.Interfaces.Repositories;

namespace TestQuestion5.Infrastructure.Repositories;

public class IdempotenciaRepository : IIdempotenciaRepository
{
    private readonly IDbConnection _dbConnection;

    public IdempotenciaRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<int> InserirIdempotenciaAsync(Idempotencia idempotencia)
    {
        string sql = "INSERT Idempotencia VALUES (@Chave_Idempotencia, @Requisicao, @Resultado)";
        return await _dbConnection.ExecuteAsync(sql, idempotencia);
    }
}