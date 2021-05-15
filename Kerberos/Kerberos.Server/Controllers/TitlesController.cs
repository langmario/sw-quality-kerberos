using Kerberos.Server.Models;
using Kerberos.Server.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
			return await _titlesService.GetAll();
		}
	}
}
