using Kerberos.Server.Database;
using Kerberos.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kerberos.Server.Controllers
{
	[Route("api/salutations")]
	[ApiController]
	public class SalutationsController : ControllerBase
	{
		private readonly KerberosContext _context;

		public SalutationsController(KerberosContext context)
		{
			_context = context;
		}

		[HttpGet]
		public async Task<IEnumerable<Salutation>> GetAllSalutations()
		{
			return await _context.Salutations.Include(s => s.Language).ToListAsync();
		}

		[HttpPost]
		public async Task<IActionResult> CreateSalutation(SalutationCreateDTO dto)
		{
			await _context.Salutations.AddAsync(new Salutation
			{
				LanguageId = dto.languageId,
				Value = dto.value,
				Gender = dto.gender
			});
			await _context.SaveChangesAsync();

			return Ok();
		}
	}

	public record SalutationCreateDTO(string value, int languageId, Gender gender);
}
