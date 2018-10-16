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

            var first = deserializedList.First(x => x.id == 1);
            first.title.Should().BeEquivalentTo("Test1");

            var second = deserializedList.First(x => x.id == 2);
            second.title.Should().BeEquivalentTo("Test2");

            var third = deserializedList.First(x => x.id == 3);
            third.title.Should().BeEquivalentTo("Test3");
        }
        private string GetTestData()
        {
            var list = new List<Album>() {
                new Album()
            {
                albumId =1,
                id =1,
                title = "Test1"
            },
                new Album()
            {
                albumId =1,
                id =2,
                title = "Test2"
            },new Album()
            {
                albumId =1,
                id =3,
                title = "Test3"
            }
            };
            
            return JsonConvert.SerializeObject(list);

        }
    }
}
