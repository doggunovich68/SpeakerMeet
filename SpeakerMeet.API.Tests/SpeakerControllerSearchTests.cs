using System;
using Xunit;
using SpeakerMeet.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace SpeakerMeet.API.Tests
{
public class SpeakerControllerSearchTests
{
        [Fact]
        public void ItExists()
        {
	    var controller = new SpeakerController();
        }
        [Fact]
        public void ItHasSearch() //Проверяем наличие метода Поиск
        {
            var controller = new SpeakerController();
            controller.Search("Jos");
        }

        [Fact]
        public void ItReturnsOkObjectResult()
        { 
            var controller = new SpeakerController();
            var result = controller.Search("Jos");
            Assert.NotNull(result);                    //Поиск принес результат?
            Assert.IsType<OkObjectResult>(result);     //Результат ОК?
        }
        [Fact]
        public void ItReturnsCollectionOfSpeakers()
        {
            var controller = new SpeakerController();
            var result = controller.Search("Jos") as OkObjectResult;
            Assert.NotNull(result);
            Assert.NotNull(result.Value);
            Assert.IsType<List<Speaker>>(result.Value);
        }
        [Fact]
        public void GivenExactMatchThenOneSpeakerInCollection()
        {
            var result = _controller.Search("Joshua") as OkObjectResult;
            var speakers = ((IEnumerable<Speaker>)result.Value).ToList();
            Assert.Equal(1, speakers.Count);
        }
	    [Theory]
	    [InlineData("Joshua")]
	    [InlineData("joshua")]
	    [InlineData("JoShUa")]
	    public void GivenCaseInsensitveMatchThenSpeakerInCollection (string searchString)
	    {
		    var result = _controller.Search(searchString) as OkObjectResult;
		    var speakers = ((IEnumerable<Speaker>)result.Value).ToList();
		    Assert.Equal(1, speakers.Count);
		    Assert.Equal("Joshua", speakers[0].Name);
        } 
        [Fact]
        public void GivenNoMatchThenEmptyCollection()
        {
            var result = _controller.Search("ZZZ") as OkObjectResult;
            var speakers = ((IEnumerable<Speaker>)result.Value).ToList();
            Assert.Equal(0, speakers.Count);
        }
        [Fact]
        public void Given3MatchThenCollectionWith3Speakers()
        {
            var result = _controller.Search("jos") as OkObjectResult;
            var speakers = ((IEnumerable<Speaker>)result.Value).ToList();
            Assert.Equal(3, speakers.Count);
            Assert.True(speakers.Any(s => s.Name == "Josh"));
            Assert.True(speakers.Any(s => s.Name == "Joshua"));
            Assert.True(speakers.Any(s => s.Name == "Joseph"));
        }
        private readonly SpeakerController _controller;
        public SpeakerControllerSearchTests()
            {
            _controller = new SpeakerController();
            }
    }
}