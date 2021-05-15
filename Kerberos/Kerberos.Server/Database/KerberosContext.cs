using Kerberos.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Kerberos.Server.Database
{
	public class KerberosContext : DbContext
	{
		public KerberosContext(DbContextOptions<KerberosContext> options) : base(options) {	}

		public DbSet<Title> Titles { get; set; }
		public DbSet<TitleAlias> TitleAliases { get; set; }
		public DbSet<Salutation> Salutations { get; set; }
		public DbSet<Language> Languages { get; set; }
	}
}
