using System;

namespace Caravela.Documentation.SampleCode.AspectFramework.LogParameters
{
    internal class TargetCode
    {
        [Log]
        private void VoidMethod(int a, out int b)
        {
            Console.WriteLine($"TargetCode.VoidMethod(a = {{{a}}}, b = <out> ) started.");
            try
            {
                b = a;
                object result = null;
                Console.WriteLine($"TargetCode.VoidMethod(a = {{{a}}}, b = <out> ) succeeded.");
                return;
            }
            catch (Exception e)
            {
                Console.WriteLine($"TargetCode.VoidMethod(a = {{{a}}}, b = <out> ) failed: {e.Message}");
                throw;
            }
        }

        [Log]
        private int IntMethod(int a)
        {
            Console.WriteLine($"TargetCode.IntMethod(a = {{{a}}}) started.");
            try
            {
                int result;
                result = a;
                goto __aspect_return_1;
            __aspect_return_1:
                Console.WriteLine($"TargetCode.IntMethod(a = {{{a}}}) returned {result}.");
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine($"TargetCode.IntMethod(a = {{{a}}}) failed: {e.Message}");
                throw;
            }
        }

    }
}