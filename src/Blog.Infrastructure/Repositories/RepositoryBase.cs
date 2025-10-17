using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Blog.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Repositories
{
	public abstract class RepositoryBase<T> : IRepository<T> where T : class
	{
		protected readonly AppDbContext Db;

		protected RepositoryBase(AppDbContext db)
		{
			Db = db;
		}

		public virtual async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
		{
			return await Db.Set<T>().FindAsync(new object[] { id }, cancellationToken);
		}

		public virtual async Task<IReadOnlyList<T>> GetAllByTenantAsync(string tenant, CancellationToken cancellationToken = default)
		{
			var query = Db.Set<T>().AsQueryable();
			if (!string.IsNullOrWhiteSpace(tenant) && typeof(T).GetProperty("Tenant") != null)
			{
				query = query.Where(e => EF.Property<string>(e, "Tenant") == tenant.ToLower());
			}
			return await query.ToListAsync(cancellationToken);
		}

		public virtual async Task AddAsync(T entity, CancellationToken cancellationToken = default)
		{
			await Db.Set<T>().AddAsync(entity, cancellationToken);
		}

		public virtual void Update(T entity)
		{
			Db.Set<T>().Update(entity);
		}

		public virtual void Delete(T entity)
		{
			Db.Set<T>().Remove(entity);
		}

		public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			return Db.SaveChangesAsync(cancellationToken);
		}
	}
}


