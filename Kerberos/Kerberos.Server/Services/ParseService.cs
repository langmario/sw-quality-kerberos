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
			// Split the incomming salutation into individual words
			IList<string> words = input.Split(" ").Where(word => !string.IsNullOrEmpty(word)).ToList();

			(result, words) = await ExtractSalutationAsync(result, words);
			(result, words) = await ExtractTitlesAsync(result, words);

			result = ExtractNames(result, words);

			return result;
		}

		/// <summary>
		/// Tries to extract a given salutation from a list of words
		/// </summary>
		/// <param name="parseResult">The previous ParseResult</param>
		/// <param name="input">List of words to search to search in</param>
		/// <returns>The next parse result</returns>
		async Task<(ParseResult, IList<string>)> ExtractSalutationAsync(ParseResult parseResult, IList<string> input)
		{
			var salutations = await _salutationsService.GetAllAsync();
			var remaining = input;

			// Check if input contains a known salutation
			foreach (var salutation in salutations)
			{
				if (salutation.Value != null && input.Contains(salutation.Value))
				{
					parseResult.Salutation = salutation;
					// Remove salutation-specific words from input words list
					remaining = input.Where(word => !salutation.Value.Equals(word)).ToList();
					break;
				}
			}

			return (parseResult, remaining);
		}

		/// <summary>
		/// Tries to extract given titles from a list of words
		/// </summary>
		/// <param name="parseResult">The previous ParseResult</param>
		/// <param name="input"></param>
		/// <returns>The next parse result</returns>
		async Task<(ParseResult, IList<string>)> ExtractTitlesAsync(ParseResult parseResult, IList<string> input)
		{
			var titles = await _titlesService.GetAllAsync();

			foreach (var title in titles)
			{
				// Sort aliases by length so that the more specific one would match first -> 'Dr. rer. nat.' before 'Dr.'
				var aliases = title.Aliases;
				aliases.Sort((a, b) => b.Value.Length - a.Value.Length);

				// Only keep aliases that are fully contained in the words list by subtracting all the words in the words list from the alias and checking if something remains
				var aliasesFound = aliases.Where(alias => !alias.Value.Split(" ").Except(input).Any()).ToList();

				if (aliasesFound.Any())
				{
					// If some aliases were found add them and remove them from the words list later
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
				// If no aliases were found check the title itself
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


		/// <summary>
		/// Tries to extract a firstname and a lastname from a given list of words
		/// </summary>
		/// <param name="parseResult">The previous ParseResult</param>
		/// <param name="names">The remaining list of words which must be part of the name</param>
		/// <returns>The final parsing result</returns>
		ParseResult ExtractNames(ParseResult parseResult, IList<string> names)
		{
			/* For name extracting we have 3 basic cases:
			 * 'Max Mustermann' -> normal name, take last word as lastname, all preceeding as firstname
			 * 'Antonius van Hoof' -> 'van' lowercase-word as an indicator for the lastname, all preceeding as firstname
			 * 'van Hoof, Antonius' -> ',' as an indicator that all preceeding words belong to the lastname, all following to the firstname
			 */

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
				parseResult.Firstname = string.Join(" ", names.Take(names.Count - 1));
				parseResult.Lastname = string.Join(" ", names.Skip(names.Count - 1));
			}

			return parseResult;
		}
	}
}
