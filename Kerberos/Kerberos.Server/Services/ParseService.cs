using Kerberos.Server.Models;
using System.Threading.Tasks;

namespace Kerberos.Server.Services
{
	public class ParseService : IParseService
	{
		private readonly SalutationsService _salutationsService;

		public ParseService(SalutationsService salutationsService)
		{
			_salutationsService = salutationsService;
		}

		public async Task<ParseResult> ParseInput(string input)
		{
			var result = new ParseResult();

			var salutations = await _salutationsService.GetAll();

			// Check if input contains a known salutation
			foreach (var salutation in salutations)
			{
				if (input.Contains(salutation.Value))
				{
					result.Salutation = salutation.Value;
					break;
				}
			}

			return result;
		}
	}
}
