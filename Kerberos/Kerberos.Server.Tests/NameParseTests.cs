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

		public NameParseTests()
		{
			_salutationsMock = new Mock<ISalutationsService>();
			_salutationsMock.Setup(s => s.GetAllAsync()).ReturnsAsync(new List<Salutation>());

			_titlesMock = new Mock<ITitlesService>();
			_titlesMock.Setup(t => t.GetAllAsync()).ReturnsAsync(new List<Title>());
		}

		[Fact]
		public async void TestNameRecognition()
		{
			IParseService parseService = new ParseService(_salutationsMock.Object, _titlesMock.Object);

			var result = await parseService.ParseInputAsync("Sandra Berger");
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
