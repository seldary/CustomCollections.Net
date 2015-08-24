using System;
using System.Collections.Generic;
using Xunit;
using System.Linq;
using Xunit.Sdk;

namespace CustomCollections.Net.Tests
{
    public class ReadonlyDictionaryTests
    {
        private readonly Dictionary<string, string> _sourceItems = new Dictionary<string, string>
        {
            {"a", "1"},
            {"b", "2"},
            {"c", "3"}
        };
        private readonly Dictionary<string, string> _emptySourceItems = new Dictionary<string, string>();

        [Fact]
        public void ReadonlyDictionaryEqualsSourceSet()
        {
            var underTest = new ReadonlyDictionary<string, string>(_sourceItems);
            Assert.Equal(_sourceItems.ToList(), underTest.ToList());
        }

        [Fact]
        public void ReadonlyDictionaryCountEqualsSourceSetCount()
        {
            var underTest = new ReadonlyDictionary<string, string>(_sourceItems);
            Assert.Equal(_sourceItems.Count, underTest.Count);
        }

        [Fact]
        public void ReadonlyDictionaryIsReadonly()
        {
            Assert.True(new ReadonlyDictionary<string, string>(_sourceItems).IsReadOnly);
        }

        [Theory]
        [InlineData("a")]
        [InlineData("b")]
        [InlineData("c")]
        [InlineData("d")]
        [InlineData("")]
        public void EmptyReadonlyDictionaryContains(string key)
        {
            var underTest = new ReadonlyDictionary<string, string>(_emptySourceItems);
            Assert.False(underTest.ContainsKey(key));
        }

        [Fact]
        public void ReadonlyDictionaryContains()
        {
            var underTest = new ReadonlyDictionary<string, string>(_sourceItems);
            Assert.True(underTest.ContainsKey("a"));
            Assert.True(underTest.ContainsKey("b"));
            Assert.True(underTest.ContainsKey("c"));
            Assert.False(underTest.ContainsKey("d"));
            Assert.False(underTest.ContainsKey(""));
        }

        [Theory]
        [InlineData("a")]
        [InlineData("b")]
        [InlineData("c")]
        public void ReadonlyDictionaryIndexerExistingItems(string key)
        {
            var underTest = new ReadonlyDictionary<string, string>(_sourceItems);
            Assert.Equal(_sourceItems[key], underTest[key]);
        }

        [Theory]
        [InlineData("a")]
        [InlineData("b")]
        [InlineData("c")]
        [InlineData("d")]
        [InlineData("")]
        public void TryGetValue(string key)
        {
            var underTest = new ReadonlyDictionary<string, string>(_sourceItems);
            string expectedValue;
            string testValue;
            Assert.Equal(_sourceItems.TryGetValue(key, out expectedValue), underTest.TryGetValue(key, out testValue));
            Assert.Equal(expectedValue, testValue);
        }

        [Fact]
        public void ReadonlyDictionaryIndexerNonExistingItems()
        {
            var underTest = new ReadonlyDictionary<string, string>(_sourceItems);
            Assert.Throws<KeyNotFoundException>(() => underTest["not"]);
        }

        [Fact]
        public void ReadonlyDictionaryCantChange()
        {
            var underTest = new ReadonlyDictionary<string, string>(new Dictionary<string, string> {{"a", "a"}});
            Assert.Throws<NotSupportedException>(() => underTest.Remove("b"));
            Assert.Throws<NotSupportedException>(() => underTest.Remove(new KeyValuePair<string, string>("b", "b")));
            Assert.Throws<NotSupportedException>(() => underTest.Add("b", "b"));
            Assert.Throws<NotSupportedException>(() => underTest.Clear());
            Assert.Throws<NotSupportedException>(() => underTest["a"] = "a");
        }
    }
}