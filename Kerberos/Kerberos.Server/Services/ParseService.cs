using Kerberos.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kerberos.Server.Services
{
	public class ParseService : IParseService
	{
		private readonly ISalutationsService _salutationsService;
		private readonly ITitlesService _titlesService;

		public ParseService(ISalutationsService salutationsService, ITitlesService titlesService)
		{
			_salutationsService = salutationsService;
			_titlesService = titlesService;
		}

		public async Task<ParseResult> ParseInputAsync(string input)
		{
			var result = new ParseResult();
			IList<string> words = input.Split(" ").Where(word => !string.IsNullOrEmpty(word)).ToList();

			(result, words) = await ExtractSalutationAsync(result, words);
			(result, words) = await ExtractTitlesAsync(result, words);

			result = ExtractNames(result, words);

			return result;
		}


		async Task<(ParseResult, IList<string>)> ExtractSalutationAsync(ParseResult parseResult, IList<string> input)
		{
			var salutations = await _salutationsService.GetAllAsync();
			var remaining = input;

			// Check if input contains a known salutation
			foreach (var salutation in salutations)
			{
				if (input.Contains(salutation.Value))
				{
					parseResult.Salutation = salutation;
					remaining = input.Where(word => !salutation.Value.Equals(word)).ToList();
					break;
				}
			}

			return (parseResult, remaining);
		}

		async Task<(ParseResult, IList<string>)> ExtractTitlesAsync(ParseResult parseResult, IList<string> input)
		{
			var titles = await _titlesService.GetAllAsync();

			foreach (var title in titles)
			{
				var aliases = title.Aliases;
				aliases.Sort((a, b) => b.Value.Length - a.Value.Length);

				var aliasesFound = aliases.Where(alias => !alias.Value.Split(" ").Except(input).Any()).ToList();

				if (aliasesFound.Any())
				{
					parseResult.Titles.Add(new Title
					{
						Id = title.Id,
						Value = title.Value,
						Aliases = aliasesFound
					});
					foreach (var found in aliasesFound)
					{
						input = input.Except(found.Value.Split(" ")).ToList();
					}
				}
				else if (!title.Value.Split(" ").Except(input).Any())
				{
					parseResult.Titles.Add(new Title
					{
						Id = title.Id,
						Value = title.Value
					});
					input = input.Except(title.Value.Split(" ")).ToList();
				}
			}

			return (parseResult, input);
		}

		ParseResult ExtractNames(ParseResult parseResult, IList<string> names)
		{
			if (names.Count == 2)
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
				if (names.Any(n => n.EndsWith(",")))
				{
					var index = names.ToList().FindIndex(n => n.EndsWith(","));
					parseResult.Lastname = string.Join(" ", names.Take(index + 1)).Replace(",", "");
					parseResult.Firstname = string.Join(" ", names.Skip(index + 1));
				}
				else if (names.Any(n => !string.IsNullOrEmpty(n) && char.IsLower(n[0])))
				{
					var index = names.ToList().FindIndex(n => !string.IsNullOrEmpty(n) && char.IsLower(n[0]));
					parseResult.Firstname = string.Join(" ", names.Take(index));
					parseResult.Lastname = string.Join(" ", names.Skip(index));
				}
				else
				{
					parseResult.Firstname = string.Join(" ", names.Take(names.Count - 2));
					parseResult.Lastname = string.Join(" ", names.Skip(names.Count - 2));
				}
			}

			return parseResult;
		}
	}
}
