using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kerberos.Server.Models
{
	public class ParseResult
	{
		public string? Salutation { get; set; }
		public IEnumerable<string> Titles { get; set; } = Enumerable.Empty<string>();
	}
}
