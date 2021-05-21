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
	public class ParseTests
	{
		private readonly Mock<ISalutationsService> _salutationsMock;
		private readonly Mock<ITitlesService> _titlesMock;

		private static readonly Language _languageGerman = new Language
		{
			Key = "de",
			Name = "Deutsch",
		};
		private static readonly Language _languageSpanish = new Language
		{
			Key = "es",
			Name = "Spanisch",
		};

		private static readonly Salutation _salutationMaleDE = new Salutation
		{
			Value = "Herr",
			Gender = Gender.MALE,
			FormalSalutation = "Sehr geehrter Herr",
			Language = _languageGerman,
		};
		private static readonly Salutation _salutationFemaleDE = new Salutation
		{
			Value = "Frau",
			Gender = Gender.FEMALE,
			FormalSalutation = "Sehr geehrte Frau",
			Language = _languageGerman,
		};

		private static readonly Salutation _salutationMaleES = new Salutation
		{
			Value = "Señor",
			Gender = Gender.MALE,
			FormalSalutation = "Estimado Señor",
			Language = _languageSpanish,
		};
		private static readonly Salutation _salutationFemaleES = new Salutation
		{
			Value = "Señora",
			Gender = Gender.FEMALE,
			FormalSalutation = "Estimada Señora",
			Language = _languageSpanish,
		};

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
						new TitleAlias
						{
							Value = "Dr.-Ing."
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

		public ParseTests()
		{
			_salutationsMock = new Mock<ISalutationsService>();
			_salutationsMock.Setup(s => s.GetAllAsync()).ReturnsAsync(new[]
			{
				_salutationMaleDE, _salutationFemaleDE, _salutationMaleES, _salutationFemaleES
			});

			_titlesMock = new Mock<ITitlesService>();
			_titlesMock.Setup(t => t.GetAllAsync()).ReturnsAsync(new[]
			{
				_titleDr, _titleProf
			});
		}


		[Fact]
		public async void TestCompleteGermanParsing()
		{
			IParseService parseService = new ParseService(_salutationsMock.Object, _titlesMock.Object);

			var result = await parseService.ParseInputAsync("Herr Prof. Dr. rer. nat. Dr.-Ing. Dr. h.c. mult. Fridolin Müller");
			Assert.Equal("Fridolin", result.Firstname);
			Assert.Equal("Müller", result.Lastname);

			Assert.Contains(result.Titles, t => t.Value == _titleDr.Value);
			Assert.Contains(result.Titles, t => t.Value == _titleProf.Value);

			Assert.Equal(_salutationMaleDE, result.Salutation);
		}

		[Fact]
		public async void TestCompleteSpanishParsing()
		{
			IParseService parseService = new ParseService(_salutationsMock.Object, _titlesMock.Object);

			var result = await parseService.ParseInputAsync("Señor Salvador Gonzales");
			Assert.Equal("Salvador", result.Firstname);
			Assert.Equal("Gonzales", result.Lastname);

			Assert.Empty(result.Titles);

			Assert.Equal(_salutationMaleES, result.Salutation);
		}

		[Fact]
		public async void TestPartialParsing()
		{
			IParseService parseService = new ParseService(_salutationsMock.Object, _titlesMock.Object);

			var result = await parseService.ParseInputAsync("Herr Tobias Raphael Meier-Müller");
			Assert.Equal("Tobias Raphael", result.Firstname);
			Assert.Equal("Meier-Müller", result.Lastname);

			Assert.Equal(_salutationMaleDE, result.Salutation);
			Assert.Empty(result.Titles);
		}
	}
}
