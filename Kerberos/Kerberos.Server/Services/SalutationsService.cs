using Kerberos.Server.Database;
using Kerberos.Server.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kerberos.Server.Services
{
	public class SalutationsService
	{
		private readonly KerberosContext _context;

		public SalutationsService(KerberosContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Salutation>> GetAllAsync()
		{
			return await _context.Salutations.Include(s => s.Language).ToListAsync();
		}

		public async Task<Salutation> AddAsync(string value, string formalSalutation, int languageId, Gender gender)
		{
			var addedEntry = await _context.Salutations.AddAsync(new Salutation
			{
				Value = value,
				FormalSalutation = formalSalutation,
				Gender = gender,
				LanguageId = languageId
			});
			await _context.SaveChangesAsync();

			return addedEntry.Entity;
		}

		public async Task RemoveAsync(int salutationId)
		{
			var salutation = await _context.Salutations.FindAsync(salutationId);
			if (salutation is null)
			{
				throw new KeyNotFoundException();
			}

			_context.Salutations.Remove(salutation);
			await _context.SaveChangesAsync();
		}
	}
}
