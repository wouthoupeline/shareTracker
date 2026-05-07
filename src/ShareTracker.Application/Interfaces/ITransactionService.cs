using ShareTracker.Application.DTOs;
using ShareTracker.Domain.Enums;

namespace ShareTracker.Application.Interfaces;

public interface ITransactionService
{
    Task<Guid> CreateAsync(CreateTransactionRequest request);
    Task<IEnumerable<TransactionResponse>> GetAllAsync();
    Task<TransactionResponse?> GetByIdAsync(Guid id);
    Task<DeleteResult> DeleteAsync(Guid id);
    Task<(UpdateResult Result, TransactionResponse? Data)> UpdateAsync(Guid id, UpdateTransactionRequest request);
}
