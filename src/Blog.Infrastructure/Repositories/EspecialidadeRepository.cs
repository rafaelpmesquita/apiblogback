using Blog.Domain.Content;

namespace Blog.Infrastructure.Repositories
{
	public sealed class EspecialidadeRepository : RepositoryBase<Especialidade>
	{
		public EspecialidadeRepository(AppDbContext db) : base(db) { }
	}
}


