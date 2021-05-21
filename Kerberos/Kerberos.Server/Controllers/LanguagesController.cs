using Kerberos.Server.Models;
using Kerberos.Server.Services;
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
		private readonly LanguagesService _languageService;

		public LanguagesController(LanguagesService languageService)
		{
			_languageService = languageService;
		}

		[HttpGet]
		public async Task<IEnumerable<Language>> GetAllLanguages()
		{
			return await _languageService.GetAllAsync();
		}

		[HttpPost]
		public async Task<ActionResult<Language>> AddLanguage(LanguageCreateDTO dto)
		{
			return await _languageService.AddAsync(dto.key, dto.name);
		}

		[HttpDelete("{languageId}")]
		public async Task<ActionResult> DeleteLanguage([FromRoute] int languageId)
		{
			try
			{
				await _languageService.RemoveAsync(languageId);
			}
			catch (KeyNotFoundException)
			{
				return NotFound();
			}
			return Ok();
		}
	}

	public record LanguageCreateDTO(string key, string name);
}
