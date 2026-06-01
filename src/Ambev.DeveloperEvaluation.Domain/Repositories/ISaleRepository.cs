using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface ISaleRepository
{
    Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Sale>> GetAllAsync(int page, int pageSize, CancellationToken cancellationToken = default);
    Task AddAsync(Sale sale, CancellationToken cancellationToken = default);
    void Update(Sale sale);
    void Delete(Sale sale);
    void SaveChanges();
}