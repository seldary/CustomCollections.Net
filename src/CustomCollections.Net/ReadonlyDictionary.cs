using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CustomCollections.Net
{
    public class ReadonlyDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        private readonly IDictionary<TKey, TValue> _dictionary;
        private readonly Entry[] _slots;
        private readonly bool _isDictionaryFallback;
        private readonly int _slotsLength;

        public ReadonlyDictionary(IDictionary<TKey, TValue> items)
        {
            _dictionary = items;
            _slotsLength = CustomCollectionsConstants.Primes.FirstOrDefault(_ => IsNoCollision(items, _));
            if (_slotsLength == 0)
            {
                _isDictionaryFallback = true;
            }
            else
            {
                _slots = new Entry[_slotsLength];

                foreach (var item in items)
                {
                    _slots[CustomCollectionsConstants.InternalGetHashCode(item.Key) % _slotsLength].Key = item.Key;
                    _slots[CustomCollectionsConstants.InternalGetHashCode(item.Key) % _slotsLength].Value = item.Value;
                }
            }
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _dictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            throw new NotSupportedException();
        }

        public void Clear()
        {
            throw new NotSupportedException();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            if (!_isDictionaryFallback)
            {
                var existingItem = _slots[CustomCollectionsConstants.InternalGetHashCode(item.Key) % _slotsLength];
                return existingItem.Key != null &&
                       item.Key.Equals(existingItem.Key) &&
                       item.Value.Equals(existingItem.Value);
            }

            return _dictionary.Contains(item);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            _dictionary.CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            throw new NotSupportedException();
        }

        public int Count => _dictionary.Count;
        public bool IsReadOnly => true;

        public bool ContainsKey(TKey key)
        {
            if (!_isDictionaryFallback)
            {
                var existingItem = _slots[CustomCollectionsConstants.InternalGetHashCode(key) % _slotsLength];
                return existingItem.Key != null && key.Equals(existingItem.Key);
            }

            return _dictionary.ContainsKey(key);
        }

        public void Add(TKey key, TValue value)
        {
            throw new NotSupportedException();
        }

        public bool Remove(TKey key)
        {
            throw new NotSupportedException();
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            if (!_isDictionaryFallback)
            {
                var existingItem = _slots[CustomCollectionsConstants.InternalGetHashCode(key) % _slotsLength];
                value = existingItem.Value;
                return existingItem.Key != null && key.Equals(existingItem.Key);
            }

            return _dictionary.TryGetValue(key, out value);
        }

        public TValue this[TKey key]
        {
            get
            {
                if (!_isDictionaryFallback)
                {
                    var existingItem = _slots[CustomCollectionsConstants.InternalGetHashCode(key) % _slotsLength];
                    if (existingItem.Key != null && key.Equals(existingItem.Key))
                    {
                        return existingItem.Value;
                    }
                }

                return _dictionary[key];

            }
            set { throw new NotSupportedException(); }
        }

        public ICollection<TKey> Keys => _dictionary.Keys;
        public ICollection<TValue> Values => _dictionary.Values;

        private bool IsNoCollision(IDictionary<TKey, TValue> items, int prime)
        {
            return items.Select(_ => CustomCollectionsConstants.InternalGetHashCode(_.Key) % prime).Distinct().Count() == items.Count;
        }

        private struct Entry
        {
            public TKey Key;
            public TValue Value;
        }
    }
}