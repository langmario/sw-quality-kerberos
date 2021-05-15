using Kerberos.Server.Models;
using System.Linq;
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

		public async Task<ParseResult> ParseInputAsync(string input)
		{
			var result = new ParseResult();
			string remaining;

			(result, remaining) = await ExtractSalutation(result, input);
			result = ExtractNames(result, remaining);

			return result;
		}


		async Task<(ParseResult result, string remaining)> ExtractSalutation(ParseResult parseResult, string input)
		{
			var salutations = await _salutationsService.GetAllAsync();

			// Check if input contains a known salutation
			foreach (var salutation in salutations)
			{
				if (input.Contains(salutation.Value))
				{
					parseResult.Salutation = salutation;
					input = input.Remove(input.IndexOf(salutation.Value), salutation.Value.Length).Trim();
					break;
				}
			}

			return (parseResult, input);
		}

		ParseResult ExtractNames(ParseResult parseResult, string input)
		{
			var names = input.Split(" ");

			if (names.Length == 2)
			{
				parseResult.Firstname = names[0];
				parseResult.Lastname = names[1];
			} else
			{
				// TODO: handle multiple names case
			}

			return parseResult;
		}
	}
}
