using Blog.Domain.Shared;

namespace Blog.Domain.Content
{
	public sealed class Especialidade : Entity
	{
		public string Lang { get; private set; }
		public string Img { get; private set; }
		public string Desc { get; private set; }
		public string Tenant { get; private set; }

		public Especialidade(string lang, string img, string desc, string tenant)
		{
			Lang = lang?.Trim().ToUpper();
			Img = img?.Trim();
			Desc = desc?.Trim();
			Tenant = tenant?.Trim().ToLower();
		}
	}
}


