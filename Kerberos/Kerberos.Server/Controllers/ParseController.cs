using Kerberos.Server.Models;
using Kerberos.Server.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Kerberos.Server.Controllers
{
	[Route("api/parse")]
	[ApiController]
	public class ParseController : ControllerBase
	{
		private readonly IParseService _parseService;

		public ParseController(IParseService parseService)
		{
			_parseService = parseService;
		}

		[HttpPost]
		public async Task<ActionResult<ParseResult>> ParseText([FromBody] string input)
		{
			return await _parseService.ParseInput(input);
		}
	}
}
