using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kerberos.Server.Controllers
{
	[Route("api/parse")]
	[ApiController]
	public class ParseController : ControllerBase
	{
		[HttpPost]
		public async Task<IActionResult> ParseText([FromBody] string input)
		{
			return Ok(input);
		}
	}
}
