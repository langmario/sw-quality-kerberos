using Kerberos.Server.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kerberos.Server.Services
{
	public interface ISalutationsService
	{
		Task<Salutation> AddAsync(string value, string formalSalutation, int languageId, Gender gender);
		Task<IEnumerable<Salutation>> GetAllAsync();
		Task RemoveAsync(int salutationId);
	}
}