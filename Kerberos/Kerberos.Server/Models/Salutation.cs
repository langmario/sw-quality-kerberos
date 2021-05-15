using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kerberos.Server.Models
{
	public class Salutation
	{
		public long Id { get; set; }
		public string Value { get; set; }
		public string LanguageKey { get; set; }
		public Gender Gender { get; set; }
	}
}
