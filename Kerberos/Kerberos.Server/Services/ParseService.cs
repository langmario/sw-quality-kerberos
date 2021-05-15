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
				if (input.Contains(title.Value))
				{
					parseResult.Titles.Add(new Title
					{
						Id = title.Id,
						Value = title.Value
					});
					input = input.Remove(input.IndexOf(title.Value), title.Value.Length).Trim();
				}
				else
				{
					var aliases = title.Aliases;
					aliases.Sort((a, b) => b.Value.Length - a.Value.Length);
					foreach (var alias in aliases)
					{
						if (input.Contains(alias.Value))
						{
							parseResult.Titles.Add(new Title
							{
								Id = title.Id,
								Value = title.Value,
								Aliases = { alias }
							});
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
				if (names[0].EndsWith(","))
				{
					parseResult.Lastname = names[0].Remove(names[0].Length - 1);
					parseResult.Firstname = names[1];
				}
				else
				{
					parseResult.Lastname = names[1];
                    parseResult.Firstname = names[0];
                }
			}
			else
			{
				for (int i = 0; i < names.Length; i++)
				{
					var name = names[i];

					// Check if name-part ends with comma (,) -> Lastname and after comma firstname
					if (name.EndsWith(","))
					{
						parseResult.Lastname = string.Join(" ", names[Range.EndAt(i + 1)]).Replace(",", "");
						parseResult.Firstname = string.Join(" ", names[Range.StartAt(i + 1)]);
						break;
					}

					// Check if a name-part starts with a lowercase letter -> prefix/suffix -> all following is lastname
					if (char.IsLower(name[0]) || i == names.Length - 1)
					{
						parseResult.Firstname = string.Join(" ", names[Range.EndAt(i)].Where(n => !n.Contains(".")));
						parseResult.Lastname = string.Join(" ", names[Range.StartAt(i)]);
						break;
					}
				}
			}

			return parseResult;
		}
	}
}
