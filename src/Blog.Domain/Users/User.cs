using System;

namespace Blog.Domain.Users
{
	public sealed class User
	{
		public Guid Id { get; private set; } = Guid.NewGuid();
		public string Email { get; private set; }
		public string PasswordHash { get; private set; }
		public string Tenant { get; private set; }

		public User(string email, string passwordHash, string tenant)
		{
			Email = email.Trim();
			PasswordHash = passwordHash;
			Tenant = tenant?.Trim().ToLower();
		}

		public void ChangePassword(string newHash)
		{
			PasswordHash = newHash;
		}
	}
}


