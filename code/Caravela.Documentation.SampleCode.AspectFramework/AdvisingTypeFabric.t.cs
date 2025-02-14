using Caravela.Framework.Aspects;
using Caravela.Framework.Fabrics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caravela.Documentation.SampleCode.AspectFramework.AdvisingTypeFabric
{
#pragma warning disable CS0067
    internal class MyClass
    {
#pragma warning disable CS0067
        class Fabric : TypeFabric
        {
            [Template]
            public int MethodTemplate() => throw new System.NotSupportedException("Compile-time only code cannot be called at run-time.");


            public override void AmendType(ITypeAmender amender) => throw new System.NotSupportedException("Compile-time only code cannot be called at run-time.");

        }
#pragma warning restore CS0067


        public int Method0()
        {
            return 0;
        }

        public int Method1()
        {
            return 1;
        }

        public int Method2()
        {
            return 2;
        }

        public int Method3()
        {
            return 3;
        }

        public int Method4()
        {
            return 4;
        }

        public int Method5()
        {
            return 5;
        }

        public int Method6()
        {
            return 6;
        }

        public int Method7()
        {
            return 7;
        }

        public int Method8()
        {
            return 8;
        }

        public int Method9()
        {
            return 9;
        }
    }
#pragma warning restore CS0067
}
