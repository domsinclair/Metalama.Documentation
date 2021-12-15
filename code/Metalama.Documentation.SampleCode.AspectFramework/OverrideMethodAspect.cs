﻿using System;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Eligibility;

namespace Metalama.Documentation.SampleCode.AspectFramework.OverrideMethodAspect_
{
    // <aspect>
    [AttributeUsage(AttributeTargets.Method)]
    public abstract class OverrideMethodAspect : Attribute, IAspect<IMethod>
    {
        public virtual void BuildAspect(IAspectBuilder<IMethod> builder)
        {
            builder.Advices.OverrideMethod(builder.Target, nameof(this.OverrideMethod));
        }

        public virtual void BuildEligibility(IEligibilityBuilder<IMethod> builder)
        {
            builder.ExceptForInheritance().MustBeNonAbstract();
        }

        public virtual void BuildAspectClass(IAspectClassBuilder builder) { }

        [Template]
        public abstract dynamic? OverrideMethod();
    }
    // </aspect>
}
