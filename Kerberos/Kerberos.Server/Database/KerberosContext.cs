using Kerberos.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Kerberos.Server.Database
{
	public class KerberosContext : DbContext
	{
		public KerberosContext(DbContextOptions<KerberosContext> options) : base(options)
		{
			Database.Migrate();
		}

		public DbSet<Title> Titles { get; set; } = null!;
		public DbSet<TitleAlias> TitleAliases { get; set; } = null!;
		public DbSet<Salutation> Salutations { get; set; } = null!;
		public DbSet<Language> Languages { get; set; } = null!;
	}
}
