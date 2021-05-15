using Kerberos.Server.Database;
using Kerberos.Server.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kerberos.Server.Services
{
	public class TitlesService
	{
		private readonly KerberosContext _context;

		public TitlesService(KerberosContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Title>> GetAllAsync()
		{
			return await _context.Titles.Include(t => t.Aliases).ToListAsync();
		}

		public async Task<Title> AddAsync(string name)
		{
			var addedEntry = await _context.Titles.AddAsync(new Title
			{
				Name = name
			});
			await _context.SaveChangesAsync();

			return addedEntry.Entity;
		}

		public async Task<Title> AddAliasToTitleAsync(int titleId, string alias)
		{
			var title = await _context.Titles.FindAsync(titleId);
			if (title is null)
			{
				throw new KeyNotFoundException();
			}

			title.Aliases.Add(new TitleAlias
			{
				Value = alias
			});

			await _context.SaveChangesAsync();

			return title;
		}
	}
}
