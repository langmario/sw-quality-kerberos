using Kerberos.Server.Models;
using Kerberos.Server.Services;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace Kerberos.Server.Tests
{
	public class NameParseTests
	{
		private readonly Mock<ISalutationsService> _salutationsMock;
		private readonly Mock<ITitlesService> _titlesMock;

		private static readonly Language _languageGerman = new Language
		{
			Key = "de",
			Name = "Deutsch",
		};
		private static readonly Salutation _salutationMale = new Salutation
		{
			Value = "Herr",
			Gender = Gender.MALE,
			FormalSalutation = "Sehr geehrter Herr",
			Language = _languageGerman,
		};

		private static readonly Salutation _salutationFemale = new Salutation
		{
			Value = "Frau",
			Gender = Gender.FEMALE,
			FormalSalutation = "Sehr geehrte Frau",
			Language = _languageGerman,
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

		public NameParseTests()
		{
			_salutationsMock = new Mock<ISalutationsService>();
			_salutationsMock.Setup(s => s.GetAllAsync()).ReturnsAsync(new[]
			{
				_salutationMale, _salutationFemale
			});

			_titlesMock = new Mock<ITitlesService>();
			_titlesMock.Setup(t => t.GetAllAsync()).ReturnsAsync(new[]
			{
				_titleDr, _titleProf
			});
		}

		[Fact]
		public async void TestNameRecognition()
		{
			IParseService parseService = new ParseService(_salutationsMock.Object, _titlesMock.Object);

			var result = await parseService.ParseInputAsync("Frau Sandra Berger");
			Assert.Equal("Sandra", result.Firstname);
			Assert.Equal("Berger", result.Lastname);
		}


		[Fact]
		public async void TestComplexNameRecognition()
		{
			IParseService parseService = new ParseService(_salutationsMock.Object, _titlesMock.Object);

			var result = await parseService.ParseInputAsync("Maria von Leuthäuser-Schnarrenberger");
			Assert.Equal("Maria", result.Firstname);
			Assert.Equal("von Leuthäuser-Schnarrenberger", result.Lastname);
		}

		[Fact]
		public async void TestComplexReverseNameRecognition()
		{
			IParseService parseService = new ParseService(_salutationsMock.Object, _titlesMock.Object);

			var result = await parseService.ParseInputAsync("von Leuthäuser-Schnarrenberger, Maria");
			Assert.Equal("Maria", result.Firstname);
			Assert.Equal("von Leuthäuser-Schnarrenberger", result.Lastname);
		}
	}
}
