using System.Collections.Generic;

namespace Kerberos.Server.Models
{
	public class Title
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public List<TitleAlias> AliasList { get; set; }
	}
}
