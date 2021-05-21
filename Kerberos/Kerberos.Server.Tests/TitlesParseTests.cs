using Kerberos.Server.Models;
using Kerberos.Server.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Kerberos.Server.Tests
{
	public class TitlesParseTests
	{
		private readonly Mock<ISalutationsService> _salutationsMock;
		private readonly Mock<ITitlesService> _titlesMock;

		private static readonly Title _titleDr = new Title
		{
			Id = 1,
			Value = "Dr.",
			Aliases = new List<TitleAlias>
					{
						new TitleAlias
						{
							Value = "Doktor"
						},
						new TitleAlias
						{
							Value = "Doktor"
						},
						new TitleAlias
						{
							Value = "Dr. rer. nat."
						},
						new TitleAlias
						{
							Value = "Dr. phil."
						},
						new TitleAlias
						{
							Value = "Dr. h.c. mult."
						},
					}
		};
		private static readonly Title _titleProf = new Title
		{
			Id = 1,
			Value = "Prof.",
			Aliases = new List<TitleAlias>
					{
						new TitleAlias
						{
							Value = "Professor"
						},
					}
		};

		public TitlesParseTests()
		{
			_salutationsMock = new Mock<ISalutationsService>();
			_salutationsMock.Setup(s => s.GetAllAsync()).ReturnsAsync(new List<Salutation>());

			_titlesMock = new Mock<ITitlesService>();
			_titlesMock.Setup(t => t.GetAllAsync()).ReturnsAsync(new[]
			{
				_titleDr, _titleProf
			});
		}

		[Fact]
		public async void TestSimpleTitleRecognition()
		{
			IParseService parseService = new ParseService(_salutationsMock.Object, _titlesMock.Object);

			var result = await parseService.ParseInputAsync("Herr Dr. Sandro Gutmensch");
			Assert.Contains(result.Titles, t => t.Value == _titleDr.Value);
		}

		[Fact]
		public async void TestComplexTitleRecognition()
		{
			IParseService parseService = new ParseService(_salutationsMock.Object, _titlesMock.Object);

			var result = await parseService.ParseInputAsync("Herr Dr. rer. nat. Sandro Gutmensch");
			Assert.Contains(result.Titles, t => t.Value == _titleDr.Value);
		}

		[Fact]
		public async void TestMultipleTitleRecognition()
		{
			IParseService parseService = new ParseService(_salutationsMock.Object, _titlesMock.Object);

			var result = await parseService.ParseInputAsync("Herr Prof. Dr. rer. nat. Dr. phil. Sandro Gutmensch");
			Assert.Contains(result.Titles, t => t.Value == "Dr.");
			Assert.Contains(result.Titles.Where(t => t.Value == _titleDr.Value).FirstOrDefault().Aliases, a => a.Value == "Dr. rer. nat.");
			Assert.Contains(result.Titles.Where(t => t.Value == _titleDr.Value).FirstOrDefault().Aliases, a => a.Value == "Dr. phil.");
			Assert.Contains(result.Titles, t => t.Value == _titleProf.Value);
		}
	}
}
