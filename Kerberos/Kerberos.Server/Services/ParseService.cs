using Kerberos.Server.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Kerberos.Server.Services
{
	public class ParseService : IParseService
	{
		private readonly SalutationsService _salutationsService;
		private readonly TitlesService _titlesService;

		public ParseService(SalutationsService salutationsService, TitlesService titlesService)
		{
			_salutationsService = salutationsService;
			_titlesService = titlesService;
		}

		public async Task<ParseResult> ParseInputAsync(string input)
		{
			var result = new ParseResult();
			var remaining = input;

			(result, remaining) = await ExtractSalutationAsync(result, remaining);
			(result, remaining) = await ExtractTitlesAsync(result, remaining);

			result = ExtractNames(result, remaining);

			return result;
		}


		async Task<(ParseResult result, string remaining)> ExtractSalutationAsync(ParseResult parseResult, string input)
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

		async Task<(ParseResult result, string remaining)> ExtractTitlesAsync(ParseResult parseResult, string input)
		{
			var titles = await _titlesService.GetAllAsync();

			foreach (var title in titles)
			{
				if (input.Contains(title.Name))
				{
					parseResult.Titles.Add(title);
					input = input.Remove(input.IndexOf(title.Name), title.Name.Length).Trim();
				}
				else
				{
					foreach (var alias in title.Aliases)
					{
						if (input.Contains(alias.Value))
						{
							parseResult.Titles.Add(title);
							input = input.Remove(input.IndexOf(alias.Value), alias.Value.Length).Trim();
							break;
						}
					}
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
			}
			else
			{
				for(int i = 0; i < names.Length; i++)
				{
					var name = names[0];
					// Check if a name-part starts with a lowercase letter
					if (char.IsLower(name[0]))
					{
						parseResult.Lastname = string.Join(" ", names[Range.StartAt(i)]);
					}
				}
			}

			return parseResult;
		}
	}
}
