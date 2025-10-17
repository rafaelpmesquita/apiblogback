using System;

namespace Blog.Domain.Shared
{
	public abstract class Entity
	{
		public Guid Id { get; protected set; } = Guid.NewGuid();
	}
}


