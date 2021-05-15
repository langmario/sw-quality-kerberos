using System.Collections.Generic;

namespace Server.Models
{
	public class Title
	{
		public string Key { get; set; }

		public List<TitleAlias> AliasList { get; set; }
	}
}
