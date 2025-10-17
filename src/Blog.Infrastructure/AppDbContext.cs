using Blog.Domain.Content;
using Blog.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure
{
	public sealed class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

		public DbSet<User> Users => Set<User>();
		public DbSet<NavItem> NavItems => Set<NavItem>();
		public DbSet<Especialidade> Especialidades => Set<Especialidade>();
		public DbSet<TimeLineItem> TimeLineItems => Set<TimeLineItem>();

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>().HasKey(x => x.Id);
			modelBuilder.Entity<NavItem>().HasKey(x => x.Id);
			modelBuilder.Entity<Especialidade>().HasKey(x => x.Id);
			modelBuilder.Entity<TimeLineItem>().HasKey(x => x.Id);
		}
	}
}


