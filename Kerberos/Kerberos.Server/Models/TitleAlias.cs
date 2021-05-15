namespace Server.Models
{
	public class TitleAlias
	{
		public long Id { get; set; }
		public string Value { get; set; }
		public Title Title { get; set; }
	}
}
