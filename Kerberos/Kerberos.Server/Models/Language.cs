using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Kerberos.Server.Models
{
	public class Language
	{
		public int Id { get; set; }
		public string Key { get; set; } = string.Empty;
		public string Name { get; set; } = string.Empty;
		[JsonIgnore]
		public IList<Salutation> Salutations { get; set; } = new List<Salutation>();
	}
}
