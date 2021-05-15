using System.Text.Json.Serialization;

namespace Kerberos.Server.Models
{
	public class TitleAlias
	{
		public int Id { get; set; }
		public string Value { get; set; } = string.Empty;

		[JsonIgnore]
		public int TitleId { get; set; }
		public Title Title { get; set; }
	}
}
