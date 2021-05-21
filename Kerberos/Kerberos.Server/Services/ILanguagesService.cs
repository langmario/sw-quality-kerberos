using Kerberos.Server.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kerberos.Server.Services
{
	public interface ILanguagesService
	{
		Task<Language> AddAsync(string key, string name);
		Task<IEnumerable<Language>> GetAllAsync();
		Task RemoveAsync(int languageId);
	}
}