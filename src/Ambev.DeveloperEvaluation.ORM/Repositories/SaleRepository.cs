using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class SaleRepository : ISaleRepository
{
    private readonly DefaultContext _context;
    public SaleRepository(DefaultContext context) => _context = context;

    public async Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await _context.Sales.Include(s => s.Items).FirstOrDefaultAsync(s => s.Id == id, cancellationToken);

    public async Task<IEnumerable<Sale>> GetAllAsync(int page, int pageSize, CancellationToken cancellationToken = default)
        => await _context.Sales.Include(s => s.Items).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

    public async Task AddAsync(Sale sale, CancellationToken cancellationToken = default)
    {
        await _context.Sales.AddAsync(sale, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public void Update(Sale sale)
    {
        _context.Sales.Update(sale);
    }

    public void Delete(Sale sale)
    {
        _context.Sales.Remove(sale);
    }

    public void SaveChanges()
    {
        _context.SaveChanges();
    }
}