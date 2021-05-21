using Kerberos.Server.Database;
using Kerberos.Server.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kerberos.Server.Services
{
	public class TitlesService : ITitlesService
	{
		private readonly KerberosContext _context;

		public TitlesService(KerberosContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Title>> GetAllAsync()
		{
			// Include 1:n relation to title aliases
			return await _context.Titles.Include(t => t.Aliases).ToListAsync();
		}

		public async Task<Title> AddAsync(string name, List<string>? aliases)
		{
			var addedEntry = await _context.Titles.AddAsync(new Title
			{
				Value = name,
				Aliases = aliases?.Select(a => new TitleAlias { Value = a }).ToList() ?? new List<TitleAlias>()
			});
			await _context.SaveChangesAsync();

			return addedEntry.Entity;
		}

		public async Task RemoveAsync(int titleId)
		{
			var title = await _context.Titles.FindAsync(titleId);
			if (title is null)
			{
				throw new KeyNotFoundException();
			}

			_context.Titles.Remove(title);

			await _context.SaveChangesAsync();
		}

		public async Task<TitleAlias> AddAliasToTitleAsync(int titleId, string alias)
		{
			// Find corresponding title first, then add the alias and save to create it
			var title = await _context.Titles.FindAsync(titleId);
			if (title is null)
			{
				throw new KeyNotFoundException();
			}

			var addedEntry = await _context.TitleAliases.AddAsync(new TitleAlias
			{
				TitleId = title.Id,
				Value = alias
			});

			await _context.SaveChangesAsync();

			return addedEntry.Entity;
		}

		public async Task RemoveAliasFromTitle(int titleId, int aliasId)
		{
			// Find title first and throw error if not found, then remove alias and throw error if not found, then save changes
			var title = await _context.Titles.Include(t => t.Aliases).FirstOrDefaultAsync(t => t.Id == titleId);
			if (title is null)
			{
				throw new KeyNotFoundException();
			}

			var alias = title.Aliases.FirstOrDefault(a => a.Id == aliasId);
			if (alias is null)
			{
				throw new KeyNotFoundException();
			}

			_context.TitleAliases.Remove(alias);
			await _context.SaveChangesAsync();
		}
	}
}
