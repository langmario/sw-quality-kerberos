namespace Kerberos.Server.Models
{
	public class TitleAlias
	{
		public int Id { get; set; }
		public string Value { get; set; }
		public Title Title { get; set; }
	}
}
