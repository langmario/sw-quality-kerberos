using System.Collections.Generic;
using System.Linq;

namespace Kerberos.Server.Models
{
	public class Title
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public List<TitleAlias> AliasList { get; set; } = new List<TitleAlias>();
	}
}
