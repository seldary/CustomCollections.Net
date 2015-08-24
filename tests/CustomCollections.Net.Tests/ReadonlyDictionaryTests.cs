using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

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
        public void GetEnumeratorEqualsSourceSetEnumerator()
        {
            var underTest = new ReadonlyDictionary<string, string>(_sourceItems);
            Assert.Equal(_sourceItems.GetEnumerator(), underTest.GetEnumerator());
        }

        [Fact]
        public void ReadonlyDictionaryEqualsSourceSet()
        {
            var underTest = new ReadonlyDictionary<string, string>(_sourceItems);
            Assert.Equal(_sourceItems.ToList(), underTest.ToList());
        }

        [Fact]
        public void ReadonlyDictionaryKeysEqualsSourceSetKeys()
        {
            var underTest = new ReadonlyDictionary<string, string>(_sourceItems);
            Assert.Equal(_sourceItems.Keys.ToList(), underTest.Keys.ToList());
        }

        [Fact]
        public void ReadonlyDictionaryValuesEqualsSourceSetValues()
        {
            var underTest = new ReadonlyDictionary<string, string>(_sourceItems);
            Assert.Equal(_sourceItems.Values.ToList(), underTest.Values.ToList());
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
            Assert.True(underTest.Contains(new KeyValuePair<string, string>("a", "1")));
            Assert.True(underTest.Contains(new KeyValuePair<string, string>("b", "2")));
            Assert.True(underTest.Contains(new KeyValuePair<string, string>("c", "3")));
            Assert.False(underTest.Contains(new KeyValuePair<string, string>("d", "4")));
            Assert.False(underTest.Contains(new KeyValuePair<string, string>("", "")));
        }

        [Fact]
        public void ReadonlyDictionaryContainsKey()
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
            Assert.Throws<NotSupportedException>(() => underTest.Add(new KeyValuePair<string, string>("b", "b")));
            Assert.Throws<NotSupportedException>(() => underTest.Clear());
            Assert.Throws<NotSupportedException>(() => underTest["a"] = "a");
        }
    }
}