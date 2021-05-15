using Kerberos.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kerberos.Server.Services
{
	public interface IParseService
	{
		Task<ParseResult> ParseInput(string input);
	}
}

