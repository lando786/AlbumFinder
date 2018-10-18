using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using AlbumFinder.Services;
using AlbumFinder.Models;
using AlbumFinder.Enums;
using AlbumFinder.Constants;

namespace AlbumFinderTests
{
    [TestClass]
    public class ClientTests
    {
        private AlbumFinderClient _underTest;
        Mock<IWebClient> client;
        [TestInitialize]
        public void Init()
        {
            client = new Mock<IWebClient>();
            _underTest = new AlbumFinderClient(client.Object);
        }
        [TestMethod]
        public void GetUriWorksWithLongId()
        {
            var uri = _underTest.GetUri("21");
            uri.AbsoluteUri.Should().BeEquivalentTo("https://jsonplaceholder.typicode.com/photos?albumId=21");
        }

        [TestMethod]
        public void NonNumbersShouldFail()
        {
            try
            {
                var uri = _underTest.GetUri("blah");

            }
            catch (ArgumentException e)
            {
                e.Message.Should().Be(ErrorMessages.InvalidInputErrorMessage);
            }
        }
        [TestMethod]
        public void SpaceShouldBeTrimmed()
        {
            var uri = _underTest.GetUri(" 2 ");
            uri.AbsoluteUri.Should().BeEquivalentTo("https://jsonplaceholder.typicode.com/photos?albumId=2");
        }
        [TestMethod]
        public void DeserializationWorking()
        {
            var deserializedList = _underTest.Deserialize(GetDeserializationTestData());
            deserializedList.Count.Should().Be(3);

            var first = deserializedList.First(x => x.Id == 1);
            first.Title.Should().BeEquivalentTo("Test1");

            var second = deserializedList.First(x => x.Id == 2);
            second.Title.Should().BeEquivalentTo("Test2");

            var third = deserializedList.First(x => x.Id == 3);
            third.Title.Should().BeEquivalentTo("Test3");
        }
        private string GetDeserializationTestData()
        {
            var list = new List<Album>() {
                new Album()
            {
                AlbumId =1,
                Id =1,
                Title = "Test1"
            },
                new Album()
            {
                AlbumId =1,
                Id =2,
                Title = "Test2"
            },new Album()
            {
                AlbumId =1,
                Id =3,
                Title = "Test3"
            }
            };
            
            return JsonConvert.SerializeObject(list);

        }

        [TestMethod]
        public void AlbumNotFoundTest()
        {
            client.Setup(x => x.GetAsync(It.IsAny<Uri>()))
                .Returns(Task.FromResult(new HttpResponseMessage() { StatusCode = HttpStatusCode.OK, Content = null}));
            var result = _underTest.GetAlbum("1").ContinueWith(x =>
            {
                x.Result.Response.Should().Be(ResponseCode.NotFound);
                x.Result.Albums.Should().BeNull();
            });
        }

        [TestMethod]
        public void InvalidInputResultTransformed()
        {
            client.Setup(x => x.GetAsync(It.IsAny<Uri>()))
               .Returns(Task.FromResult(new HttpResponseMessage() {
                   StatusCode = HttpStatusCode.BadRequest,
                   
               }));
            var result = _underTest.GetAlbum("asdfadsf").ContinueWith(x =>
            {
                x.Result.Response.Should().Be(ResponseCode.InvalidInput);
                x.Result.Albums.Should().BeNull();
            });
        }

        [TestMethod]
        public void ResultsReturned()
        {
            client.Setup(x => x.GetAsync(It.IsAny<Uri>()))
               .Returns(Task.FromResult(new HttpResponseMessage() {
                   StatusCode = HttpStatusCode.OK,
                   Content = new StringContent(GetDeserializationTestData())
               }));
            var result = _underTest.GetAlbum("asdfadsf").ContinueWith(x =>
            {
                var res = x.Result;
                res.Response.Should().Be(ResponseCode.Ok);
                res.Albums.First(a => a.Id == 1).Title.Should().Be("Test1");
                res.Albums.First(a => a.Id == 2).Title.Should().Be("Test2");
                res.Albums.First(a => a.Id == 3).Title.Should().Be("Test3");

            });
        }
    }
}
