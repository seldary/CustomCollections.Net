using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace CustomCollections.Net.Benchmarks
{
    internal class A0 { };
    internal class A1 { };
    internal class A2 { };
    internal class A3 { };
    internal class A4 { };
    internal class A5 { };
    internal class A6 { };
    internal class A7 { };
    internal class A8 { };
    internal class A9 { };
    internal class A10 { };
    internal class A11 { };
    internal class A12 { };
    internal class A13 { };
    internal class A14 { };
    internal class A15 { };
    internal class A16 { };
    internal class A17 { };
    internal class A18 { };
    internal class A19 { };
    internal class A20 { };
    internal class A21 { };
    internal class A22 { };
    internal class A23 { };
    internal class A24 { };
    internal class A25 { };
    internal class A26 { };
    internal class A27 { };
    internal class A28 { };
    internal class A29 { };
    internal class A30 { };
    internal class A31 { };
    internal class A32 { };
    internal class A33 { };
    internal class A34 { };
    internal class A35 { };
    internal class A36 { };
    internal class A37 { };
    internal class A38 { };
    internal class A39 { };

    public static class HashSetBenchmark
    {
        private static readonly object[] Objects = {
            new A0(),
            new A1(),
            new A2(),
            new A3(),
            new A4(),
            new A5(),
            new A6(),
            new A7(),
            new A8(),
            new A9(),
            new A10(),
            new A11(),
            new A12(),
            new A13(),
            new A14(),
            new A15(),
            new A16(),
            new A17(),
            new A18(),
            new A19(),
            new A20(),
            new A21(),
            new A22(),
            new A23(),
            new A24(),
            new A25(),
            new A26(),
            new A27(),
            new A28(),
            new A29(),                
            new A30(),
            new A31(),
            new A32(),
            new A33(),
            new A34(),
            new A35(),
            new A36(),
            new A37(),
            new A38(),
            new A39(),
        };

        private static readonly List<Type> Types = Objects.Select(_ => _.GetType()).ToList();
        private static readonly int _hitCount = (int)(Types.Count * 0.5);
        private static Dictionary<Type, Type> Dic;
        private static ReadonlyDictionary<Type, Type> ReadonlyDictionary;

        static void Main()
        {
            Dic = Types.Take(_hitCount).ToDictionary(_ => _);
            ReadonlyDictionary = new ReadonlyDictionary<Type, Type>(Dic);

            long dictionaryCount = 0;
            long readonlyDictionaryCount = 0;

            for (int i = 0; i < 10; i++)
            {
                dictionaryCount += TimeDictionary(Dic);
                readonlyDictionaryCount += TimeDictionary(ReadonlyDictionary);
            }

            Console.WriteLine("Dictionary took: " + dictionaryCount);
            Console.WriteLine("ReadonlyDictionary took: " + readonlyDictionaryCount);
            Console.WriteLine("Improvement: " + (double)readonlyDictionaryCount / dictionaryCount);
        }

        private static long TimeDictionary(IDictionary<Type, Type> hashSet)
        {
            var counter = 0;
            var stopwatch = Stopwatch.StartNew();
            for (var i = 0; i < 10000000; i++)
            {
                Type type;
                if (hashSet.TryGetValue(Types[i % Types.Count], out type)) counter++;
            }
            
            return stopwatch.ElapsedMilliseconds;
        }
    }
}
