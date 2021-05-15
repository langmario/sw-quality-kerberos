using Kerberos.Server.Models;
using Kerberos.Server.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kerberos.Server.Controllers
{
	[Route("api/titles")]
	[ApiController]
	public class TitlesController : ControllerBase
	{
		private readonly TitlesService _titlesService;

		public TitlesController(TitlesService titlesService)
		{
			_titlesService = titlesService;
		}

		[HttpGet]
		public async Task<IEnumerable<Title>> GetAllTitles()
		{
			return await _titlesService.GetAllAsync();
		}

		[HttpPost]
		public async Task<ActionResult<Title>> AddTitle(TitleCreateDTO dto)
		{
			return await _titlesService.AddAsync(dto.name);
		}

		[HttpPost("{titleId}/aliases")]
		public async Task<ActionResult<TitleAlias>> AddAlias([FromRoute] int titleId, [FromBody] TitleAliasCreateDTO dto)
		{
			return await _titlesService.AddAliasToTitleAsync(titleId, dto.alias);
		}
	}

	public record TitleCreateDTO(string name);
	public record TitleAliasCreateDTO(string alias);
}
