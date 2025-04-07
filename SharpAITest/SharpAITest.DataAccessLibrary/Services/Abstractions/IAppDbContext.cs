using System.Data;

namespace SharpAITest.DataAccessLibrary.Services.Abstractions;

public interface IAppDbContext
{
    IDbTransaction Transaction { get; }
    Task<bool> BeginTransactionAsync();
    Task CommitAsync();
    Task RollbackAsync();
}