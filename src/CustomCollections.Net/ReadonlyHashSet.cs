using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CustomCollections.Net
{
    public sealed class ReadonlyHashSet<T> : ISet<T>
    {
        private readonly HashSet<T> _hashSet;
        private readonly T[] _slots;
        private readonly bool _isHashSetFallback;
        private readonly int _slotsLength;

        public ReadonlyHashSet(ISet<T> items)
        {
            _hashSet = new HashSet<T>(items);
            _slotsLength = CustomCollectionsConstants.Primes.FirstOrDefault(_ => IsNoCollision(items, _));
            if (_slotsLength == 0)
            {
                _isHashSetFallback = true;
            }
            else
            {
                _slots = new T[_slotsLength];

                foreach (var item in items)
                {
                    _slots[CustomCollectionsConstants.InternalGetHashCode(item) % _slots.Length] = item;
                }
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _hashSet.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        void ICollection<T>.Add(T item)
        {
            throw new NotSupportedException();
        }

        public void UnionWith(IEnumerable<T> other)
        {
            throw new NotSupportedException();
        }

        public void IntersectWith(IEnumerable<T> other)
        {
            throw new NotSupportedException();
        }

        public void ExceptWith(IEnumerable<T> other)
        {
            throw new NotSupportedException();
        }

        public void SymmetricExceptWith(IEnumerable<T> other)
        {
            throw new NotSupportedException();
        }

        public bool IsSubsetOf(IEnumerable<T> other)
        {
            return _hashSet.IsSubsetOf(other);
        }

        public bool IsSupersetOf(IEnumerable<T> other)
        {
            return _hashSet.IsSupersetOf(other);
        }

        public bool IsProperSupersetOf(IEnumerable<T> other)
        {
            return _hashSet.IsProperSupersetOf(other);
        }

        public bool IsProperSubsetOf(IEnumerable<T> other)
        {
            return _hashSet.IsProperSubsetOf(other);
        }

        public bool Overlaps(IEnumerable<T> other)
        {
            return _hashSet.Overlaps(other);
        }

        public bool SetEquals(IEnumerable<T> other)
        {
            return _hashSet.SetEquals(other);
        }

        public bool Add(T item)
        {
            throw new NotSupportedException();
        }

        public void Clear()
        {
            throw new NotSupportedException();
        }

        public bool Contains(T item)
        {
            if (!_isHashSetFallback)
            {
                var existingItem = _slots[CustomCollectionsConstants.InternalGetHashCode(item)%_slotsLength];
                return existingItem != null && item.Equals(existingItem);
            }

            return _hashSet.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _hashSet.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            throw new NotSupportedException();
        }

        public int Count => _hashSet.Count;
        public bool IsReadOnly => true;

        private bool IsNoCollision(ISet<T> items, int prime)
        {
            return items.Select(_ => CustomCollectionsConstants.InternalGetHashCode(_) % prime).Distinct().Count() == items.Count;
        }
    }
}