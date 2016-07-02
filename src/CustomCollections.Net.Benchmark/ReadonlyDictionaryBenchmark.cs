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

        [Params("test 2", "test not contains")]
        public string Key { get; set; }
        private readonly ReadonlyDictionary<string, int> _readonlyDictionary;
        private readonly Dictionary<string, int> _dictionary;
        private readonly ConcurrentDictionary<string, int> _concurrentDictionary;
        private readonly  ImmutableDictionary<string, int> _immutableDictionary;

        #endregion

        #region Constructors

        public ReadonlyDictionaryBenchmark()
        {
            _dictionary = Enumerable.Range(1, 4).ToDictionary(_ => "test " + _, _ => _);
            _readonlyDictionary = new ReadonlyDictionary<string, int>(_dictionary);
            _concurrentDictionary = new ConcurrentDictionary<string, int>(_dictionary);
            _immutableDictionary = _dictionary.ToImmutableDictionary();
        }

        #endregion

        #region Methods

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