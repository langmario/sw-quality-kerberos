using Kerberos.Server.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kerberos.Server.Services
{
	public interface ITitlesService
	{
		Task<TitleAlias> AddAliasToTitleAsync(int titleId, string alias);
		Task<Title> AddAsync(string name, List<string>? aliases);
		Task<IEnumerable<Title>> GetAllAsync();
		Task RemoveAliasFromTitle(int titleId, int aliasId);
		Task RemoveAsync(int titleId);
	}
}