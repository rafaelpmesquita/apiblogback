using Blog.Domain.Shared;

namespace Blog.Domain.Content
{
	public sealed class NavItem : Entity
	{
		public string Texto { get; private set; }
		public string Rota { get; private set; }
		public string Tenant { get; private set; }

		public NavItem(string texto, string rota, string tenant)
		{
			Texto = texto?.Trim();
			Rota = rota?.Trim();
			Tenant = tenant?.Trim().ToLower();
		}
	}
}


