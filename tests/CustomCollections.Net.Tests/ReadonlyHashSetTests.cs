using System;
using System.Collections.Generic;
using Xunit;

namespace CustomCollections.Net.Tests
{
    public class ReadonlyHashSetTests
    {
        private readonly HashSet<string> _sourceItems = new HashSet<string> { "a", "b", "c" };
        private readonly HashSet<string> _emptySourceItems = new HashSet<string>();

        [Fact]
        public void ReadonlyHashSetEqualsSourceSet()
        {
            var underTest = new ReadonlyHashSet<string>(_sourceItems);
            Assert.Equal(_sourceItems, underTest);
        }

        [Fact]
        public void ReadonlyHashSetCountEqualsSourceSetCount()
        {
            var underTest = new ReadonlyHashSet<string>(_sourceItems);
            Assert.Equal(_sourceItems.Count, underTest.Count);
        }

        [Fact]
        public void ReadonlyHashSetIsReadonly()
        {
            Assert.True(new ReadonlyHashSet<string>(_sourceItems).IsReadOnly);
        }

        [Fact]
        public void EmptyReadonlyHashSetContains()
        {
            var underTest = new ReadonlyHashSet<string>(_emptySourceItems);
            Assert.False(underTest.Contains("a"));
            Assert.False(underTest.Contains("b"));
            Assert.False(underTest.Contains("c"));
            Assert.False(underTest.Contains("d"));
            Assert.False(underTest.Contains(""));
        }

        [Fact]
        public void ReadonlyHashSetContains()
        {
            var underTest = new ReadonlyHashSet<string>(_sourceItems);
            Assert.True(underTest.Contains("a"));
            Assert.True(underTest.Contains("b"));
            Assert.True(underTest.Contains("c"));
            Assert.False(underTest.Contains("d"));
            Assert.False(underTest.Contains(""));
        }

        [Fact]
        public void ReadonlyHashSetCantChange()
        {
            var underTest = new ReadonlyHashSet<string>(new HashSet<string> { "a" });
            Assert.Throws<NotSupportedException>(() => underTest.Remove("b"));
            Assert.Throws<NotSupportedException>(() => underTest.Add("b"));
            Assert.Throws<NotSupportedException>(() => underTest.Clear());
            Assert.Throws<NotSupportedException>(() => underTest.ExceptWith(new HashSet<string> { "a" }));
            Assert.Throws<NotSupportedException>(() => underTest.UnionWith(new HashSet<string> { "a" }));
            Assert.Throws<NotSupportedException>(() => underTest.IntersectWith(new HashSet<string> { "a" }));
            Assert.Throws<NotSupportedException>(() => underTest.SymmetricExceptWith(new HashSet<string> { "a" }));
        }
    }
}
