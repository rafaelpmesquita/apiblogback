using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Blog.Domain.Abstractions
{
	public interface IRepository<T>
	{
		Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
		Task<IReadOnlyList<T>> GetAllByTenantAsync(string tenant, CancellationToken cancellationToken = default);
		Task AddAsync(T entity, CancellationToken cancellationToken = default);
		void Update(T entity);
		void Delete(T entity);
		Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
	}
}


