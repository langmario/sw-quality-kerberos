using System.Text.Json.Serialization;

namespace Kerberos.Server.Models
{
	public class Salutation
	{
		public int Id { get; set; }
		public string Value { get; set; } = string.Empty;
		[JsonIgnore]
		public int LanguageId { get; set; }
		public Language Language { get; set; }
		public Gender Gender { get; set; }
	}
}
