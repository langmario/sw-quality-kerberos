using Kerberos.Server.Database;
using Kerberos.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kerberos.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TitlesController : ControllerBase
	{
		private readonly KerberosContext _context;

		public TitlesController(KerberosContext context)
		{
			_context = context;
		}

		[HttpGet]
		public async Task<IEnumerable<Title>> GetAllTitles()
		{
			return await _context.Titles.Include(t => t.AliasList).ToListAsync();
		}
	}
}
