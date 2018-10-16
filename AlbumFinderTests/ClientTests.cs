using System;
using System.Collections.Generic;
using System.Linq;
using AlbumFinder;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace AlbumFinderTests
{
    [TestClass]
    public class ClientTests
    {
        private AlbumFinderClient _underTest;
        [TestInitialize]
        public void Init()
        {
            _underTest = new AlbumFinderClient();
        }
        [TestMethod]
        public void GetUriWorksWithLongId()
        {
            var uri = _underTest.GetUri("21");
            uri.AbsoluteUri.Should().BeEquivalentTo("https://jsonplaceholder.typicode.com/photos?albumId=21");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NonNumbersShouldFail()
        {
            var uri = _underTest.GetUri("blah");
            uri.Should().BeNull();
        }
        [TestMethod]
        public void DeserializationWorking()
        {
            var deserializedList = _underTest.Deserialize(GetTestData());
            deserializedList.Count.Should().Be(3);

            var first = deserializedList.First(x => x.Id == 1);
            first.Title.Should().BeEquivalentTo("Test1");

            var second = deserializedList.First(x => x.Id == 2);
            second.Title.Should().BeEquivalentTo("Test2");

            var third = deserializedList.First(x => x.Id == 3);
            third.Title.Should().BeEquivalentTo("Test3");
        }
        private string GetTestData()
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
    }
}
