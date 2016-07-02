using BenchmarkDotNet.Attributes;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace CustomCollections.Net.Benchmark
{
    public class ReadonlyDictionaryBenchmark
    {
        #region Data Members

        private ConcurrentDictionary<string, int> _concurrentDictionary;
        private Dictionary<string, int> _dictionary;
        private ImmutableDictionary<string, int> _immutableDictionary;
        private ReadonlyDictionary<string, int> _readonlyDictionary;

        #endregion

        #region Properties

        [Params("test 2", "test not contains")]
        public string Key { get; set; }

        [Params(4, 64)]
        public int Size { get; set; }

        #endregion
        
        #region Methods

        [Setup]
        public void Setup()
        {
            _dictionary = Enumerable.Range(1, Size).ToDictionary(_ => "test " + _, _ => _);
            _readonlyDictionary = new ReadonlyDictionary<string, int>(_dictionary);
            _concurrentDictionary = new ConcurrentDictionary<string, int>(_dictionary);
            _immutableDictionary = _dictionary.ToImmutableDictionary();
        }


        [Benchmark]
        public bool ReadonlyDictionary4Items()
        {
            int value;
            return _readonlyDictionary.TryGetValue(Key, out value);
        }

        [Benchmark]
        public bool Dictionary4Items()
        {
            int value;
            return _dictionary.TryGetValue(Key, out value);
        }

        [Benchmark]
        public bool ConcurrentDictionary4Items()
        {
            int value;
            return _concurrentDictionary.TryGetValue(Key, out value);
        }

        [Benchmark]
        public bool ImmutableDictionary4Items()
        {
            int value;
            return _immutableDictionary.TryGetValue(Key, out value);
        }

        #endregion
    }
}