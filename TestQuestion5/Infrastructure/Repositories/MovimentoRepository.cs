using Dapper;
using System.Data;
using TestQuestion5.Domain.Entities;
using TestQuestion5.Domain.Interfaces.Repositories;
using static Dapper.SqlMapper;

namespace TestQuestion5.Infrastructure.Repositories;

public class MovimentoRepository : IMovimentoRepository
{
    private readonly IDbConnection _dbConnection;

    public MovimentoRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<int> InserirMovimentoAsync(Movimento movimento)
    {
        string sql = "INSERT INTO Movimento VALUES (@IdMovimento, @IdContaCorrente, @DataMovimento, @TipoMovimento, @Valor)";
        return await _dbConnection.ExecuteAsync(sql, movimento);
    }

    public async Task<decimal> GetSaldoAtualAsync(string idContaCorrente)
    {
        string sqlCreditos = "SELECT SUM(valor) FROM movimento WHERE idcontacorrente = @idContaCorrente AND tipomovimento = 'C'";
        string sqlDebitos = "SELECT SUM(Valor) FROM movimento WHERE idcontacorrente = @idContaCorrente AND tipomovimento = 'D'";

        var somaCreditos = await _dbConnection.ExecuteScalarAsync<decimal?>(sqlCreditos, new { idContaCorrente }) ?? 0m;
        var somaDebitos = await _dbConnection.ExecuteScalarAsync<decimal?>(sqlDebitos, new { idContaCorrente }) ?? 0m;

        decimal saldo = somaCreditos - somaDebitos;

        return saldo;
    }
}