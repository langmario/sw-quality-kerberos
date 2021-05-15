using Kerberos.Server.Database;
using Kerberos.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kerberos.Server.Controllers
{
	[Route("api/languages")]
	[ApiController]
	public class LanguagesController : ControllerBase
	{
		private readonly KerberosContext _context;

		public LanguagesController(KerberosContext context)
		{
			_context = context;
		}

		[HttpGet]
		public async Task<IEnumerable<Language>> GetAllLanguages()
		{
			return await _context.Languages.ToListAsync();
		}

		[HttpPost]
		public async Task<IActionResult> AddLanguage(LanguageCreateDTO dto)
		{
			await _context.Languages.AddAsync(new Language
			{
				Key = dto.key.ToLower(),
				Name = dto.name
			});
			await _context.SaveChangesAsync();

			return Ok();
		}
	}

	public record LanguageCreateDTO(string key, string name);
}
