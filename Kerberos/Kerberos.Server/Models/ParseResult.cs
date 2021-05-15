using System.Collections.Generic;

namespace Kerberos.Server.Models
{
	public class ParseResult
	{
		public string Firstname { get; set; } = string.Empty;
		public string Lastname { get; set; } = string.Empty;
		public Salutation? Salutation { get; set; }
		public List<Title> Titles { get; set; } = new List<Title>();

	}
}
