using Blog.Domain.Shared;

namespace Blog.Domain.Content
{
	public sealed class TimeLineItem : Entity
	{
		public string Titulo { get; private set; }
		public string Desc { get; private set; }
		public string Lado { get; private set; }
		public string Tenant { get; private set; }

		public TimeLineItem(string titulo, string desc, string lado, string tenant)
		{
			Titulo = titulo?.Trim();
			Desc = desc?.Trim();
			Lado = lado?.Trim().ToLower();
			Tenant = tenant?.Trim().ToLower();
		}
	}
}


