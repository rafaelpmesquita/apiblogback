using Blog.Domain.Content;

namespace Blog.Infrastructure.Repositories
{
	public sealed class TimeLineRepository : RepositoryBase<TimeLineItem>
	{
		public TimeLineRepository(AppDbContext db) : base(db) { }
	}
}


