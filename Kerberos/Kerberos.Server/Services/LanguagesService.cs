using Kerberos.Server.Database;
using Kerberos.Server.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kerberos.Server.Services
{
	public class LanguagesService
	{
		private readonly KerberosContext _context;

		public LanguagesService(KerberosContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Language>> GetAllAsync()
		{
			return await _context.Languages.ToListAsync();
		}

		public async Task<Language> AddAsync(string key, string name)
		{
			var addedEntry = await _context.Languages.AddAsync(new Language
			{
				Key = key.ToLower(),
				Name = name
			});
			await _context.SaveChangesAsync();

			return addedEntry.Entity;
		}

		public async Task RemoveAsync(int languageId)
		{
			var language = await _context.Languages.FindAsync(languageId);
			if (language is null)
			{
				throw new KeyNotFoundException();
			}
			_context.Languages.Remove(language);

			await _context.SaveChangesAsync();
		}
	}
}
