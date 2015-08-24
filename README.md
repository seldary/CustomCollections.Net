# CustomCollections.Net

## ReadonlyDictionary & ReadonlyHashSet
 - Acts exactly like Dictionary and HashSet, except they don't support any changes after initialization.
 - both built just like their .Net counterpart, with an array acting as a hash map. Given a constant set of items, these collections choose a small prime number for the hash map with no collisions. If such a number can't be found, the general purpose Dictionary and HashSet are used.
 - These collections are O(1) (*not* amortized), as there are no collisions by definition.

### Why and when are they helpful?
When you have no writes, and a lot of reads, these collections can shave 10% - 50% of the Dictionary / HashSet performance. Always benchmark your code.
