using Kerberos.Server.Models;
using Kerberos.Server.Services;
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
		private readonly SalutationsService _salutationsService;

		public SalutationsController(SalutationsService salutationsService)
		{
			_salutationsService = salutationsService;
		}

		[HttpGet]
		public async Task<IEnumerable<Salutation>> GetAllSalutations()
		{
			return await _salutationsService.GetAllAsync();
		}

		[HttpPost]
		public async Task<ActionResult<Salutation>> CreateSalutation(SalutationCreateDTO dto)
		{
			return await _salutationsService.AddAsync(dto.value, dto.formalSalutation, dto.languageId, dto.gender);
		}
	}

	public record SalutationCreateDTO(string value, string formalSalutation, int languageId, Gender gender);
}
