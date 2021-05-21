using System.Collections.Generic;
using System.Linq;

namespace Kerberos.Server.Models
{
	public class Title
	{
		public int Id { get; set; }
		public string Value { get; set; } = string.Empty;
		public List<TitleAlias> Aliases { get; set; } = new List<TitleAlias>();
	}
}
