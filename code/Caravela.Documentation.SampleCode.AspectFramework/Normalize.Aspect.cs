﻿using System;
using Caravela.Framework.Aspects;
using Caravela.Framework.Code;

namespace Caravela.Documentation.SampleCode.AspectFramework.Normalize
{
    class NormalizeAttribute : FieldOrPropertyAspect
    {
        public override void BuildAspect( IAspectBuilder<IFieldOrProperty> builder )
        {
            builder.Advices.OverrideFieldOrProperty(builder.Target, nameof(this.OverrideProperty));
        }

        [Template]
        string OverrideProperty
        {
            set => meta.Target.FieldOrProperty.Value = value?.Trim().ToLowerInvariant();
        }
    }
}
