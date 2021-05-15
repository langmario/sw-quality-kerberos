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

		public async Task<IEnumerable<Title>> GetAll()
		{
			return await _context.Titles.Include(t => t.AliasList).ToListAsync();
		}

		public async Task<Title> Add(string name)
		{
			var addedEntry = await _context.Titles.AddAsync(new Title
			{
				Name = name
			});
			await _context.SaveChangesAsync();

			return addedEntry.Entity;
		}
	}
}
