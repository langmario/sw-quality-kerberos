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
			return await _languageService.GetAll();
		}

		[HttpPost]
		public async Task<ActionResult<Language>> AddLanguage(LanguageCreateDTO dto)
		{
			return await _languageService.Add(dto.key, dto.name);
		}
	}

	public record LanguageCreateDTO(string key, string name);
}
