using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caravela.Framework.RunTime;

namespace Caravela.Documentation.SampleCode.AspectFramework.EmptyOverrideMethodAllTemplates
{

    public class Program
    {
        public static async Task Main()
        {
            NormalMethod();

            await AsyncMethod();

            foreach (var a in EnumerableMethod())
            {
                Console.WriteLine($" Received {a} from EnumerableMethod");
            }

            var enumerator = EnumeratorMethod();
            while (enumerator.MoveNext())
            {
                Console.WriteLine($" Received {enumerator.Current} from EnumeratorMethod");
            }

            await foreach (var a in AsyncEnumerableMethod())
            {
                Console.WriteLine($" Received {a} from AsyncEnumerableMethod");
            }

            var asyncEnumerator = AsyncEnumeratorMethod();
            while (await asyncEnumerator.MoveNextAsync())
            {
                Console.WriteLine($" Received {asyncEnumerator.Current} from AsyncEnumeratorMethod");
            }
        }

        [EmptyOverrideMethodAspect]
        public static int NormalMethod()
        {
            Console.WriteLine($"NormalMethod: start");
            int result;
            result = 5;
            goto __aspect_return_1;
        __aspect_return_1:
            Console.WriteLine($"NormalMethod: returning {result}.");
            return result;
        }

        [EmptyOverrideMethodAspect]
        public static async Task<int> AsyncMethod()
        {
            Console.WriteLine($"AsyncMethod: start");
            var result = await AsyncMethod_Source();
            Console.WriteLine($"AsyncMethod: returning {result}.");
            return result;
        }

        private static async Task<int> AsyncMethod_Source()
        {
            Console.WriteLine("  Task.Yield");
            await Task.Yield();
            Console.WriteLine("  Resuming");
            return 5;
        }

        [EmptyOverrideMethodAspect]
        public static IEnumerable<int> EnumerableMethod()
        {
            Console.WriteLine($"EnumerableMethod: start");
            var result = EnumerableMethod_Source().Buffer();
            Console.WriteLine($"EnumerableMethod: returning {result}.");
            return result;
        }

        private static IEnumerable<int> EnumerableMethod_Source()
        {
            Console.WriteLine("  Yielding 1");
            yield return 1;
            Console.WriteLine("  Yielding 2");
            yield return 2;
            Console.WriteLine("  Yielding 3");
            yield return 3;
        }

        [EmptyOverrideMethodAspect]
        public static IEnumerator<int> EnumeratorMethod()
        {
            Console.WriteLine($"EnumeratorMethod: start");
            var result = EnumeratorMethod_Source().Buffer();
            Console.WriteLine($"EnumeratorMethod: returning {result}.");
            return result;
        }

        private static IEnumerator<int> EnumeratorMethod_Source()
        {
            Console.WriteLine("  Yielding 1");
            yield return 1;
            Console.WriteLine("  Yielding 2");
            yield return 2;
            Console.WriteLine("  Yielding 3");
            yield return 3;
        }

        [EmptyOverrideMethodAspect]
        public static async IAsyncEnumerable<int> AsyncEnumerableMethod()
        {
            Console.WriteLine($"AsyncEnumerableMethod: start");
            var result = await AsyncEnumerableMethod_Source().BufferAsync();
            Console.WriteLine($"AsyncEnumerableMethod: returning {result}.");
            await foreach (var r in result)
            {
                yield return r;
            }

            yield break;
        }

        private static async IAsyncEnumerable<int> AsyncEnumerableMethod_Source()
        {
            await Task.Yield();
            Console.WriteLine("  Yielding 1");
            yield return 1;
            Console.WriteLine("  Yielding 2");
            yield return 2;
            Console.WriteLine("  Yielding 3");
            yield return 3;
        }

        [EmptyOverrideMethodAspect]
        public static async IAsyncEnumerator<int> AsyncEnumeratorMethod()
        {
            Console.WriteLine($"AsyncEnumeratorMethod: start");
            var result = await AsyncEnumeratorMethod_Source().BufferAsync();
            Console.WriteLine($"AsyncEnumeratorMethod: returning {result}.");
            var enumerator = result;
            try
            {
                while (await enumerator.MoveNextAsync())
                {
                    yield return enumerator.Current;
                }
            }
            finally
            {
                await enumerator.DisposeAsync();
            }

            yield break;
        }

        private static async IAsyncEnumerator<int> AsyncEnumeratorMethod_Source()
        {
            await Task.Yield();
            Console.WriteLine("  Yielding 1");
            yield return 1;
            Console.WriteLine("  Yielding 2");
            yield return 2;
            Console.WriteLine("  Yielding 3");
            yield return 3;
        }
    }
}
