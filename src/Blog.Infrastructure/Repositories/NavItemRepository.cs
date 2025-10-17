using Blog.Domain.Content;

namespace Blog.Infrastructure.Repositories
{
	public sealed class NavItemRepository : RepositoryBase<NavItem>
	{
		public NavItemRepository(AppDbContext db) : base(db) { }
	}
}


