﻿using System;

namespace Metalama.Documentation.SampleCode.AspectFramework.ConvertToRunTime
{
    internal class TargetCode
    {
        [ConvertToRunTimeAspect]
        private void Method(string a, int c, DateTime e) 
        {
            Console.WriteLine($"Method({a}, {c}, {e})");
        }
    }
}
