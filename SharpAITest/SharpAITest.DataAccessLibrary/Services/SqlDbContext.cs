using SharpAITest.DataAccessLibrary.Services.Abstractions;
using System.Data.Common;
using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;

namespace SharpAITest.DataAccessLibrary.Services;

public class SqlDbContext : IAppDbContext
{
    private readonly Lazy<Task<IDbConnection>> lazyConnection;
    public Task<IDbConnection> Connection => lazyConnection.Value;
    public IDbTransaction Transaction { get; private set; }

    public SqlDbContext(IConfiguration config)
    {
        lazyConnection = new Lazy<Task<IDbConnection>>(async () =>
        {
            var connectionString = config.GetConnectionString("Default");
            var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();
            return connection;
        });
    }

    public async Task<bool> BeginTransactionAsync()
    {
        if (Transaction is not null)
            return false;

        var connection = await Connection;
        if (connection is DbConnection dbConnection)
        {
            Transaction = await dbConnection.BeginTransactionAsync();
        }
        else
        {
            Transaction = Connection.Result.BeginTransaction();
        }
        return true;
    }

    public async Task CommitAsync()
    {
        try
        {
            if (Transaction is DbTransaction dbTransaction)
            {
                await dbTransaction.CommitAsync();
            }
            else
            {
                Transaction.Commit();
            }
        }
        finally
        {
            Transaction?.Dispose();
        }
    }

    public async Task RollbackAsync()
    {
        try
        {
            if (Transaction is DbTransaction dbTransaction)
            {
                await dbTransaction.RollbackAsync();
            }
            else
            {
                Transaction?.Rollback();
            }
        }
        finally
        {
            Transaction?.Dispose();
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (Transaction is not null)
        {
            if (Transaction is IAsyncDisposable transactionAsyncDisposable)
            {
                await transactionAsyncDisposable.DisposeAsync();
            }
            else
            {
                Transaction?.Dispose();
            }
        }

        if (lazyConnection.IsValueCreated)
        {
            var connection = await Connection;
            if (connection is IAsyncDisposable connectionAsyncDisposable)
            {
                await connectionAsyncDisposable.DisposeAsync();
            }
            else
            {
                connection?.Dispose();
            }
        }

        GC.SuppressFinalize(this);
    }
}
