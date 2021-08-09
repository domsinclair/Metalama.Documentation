﻿using System;
using System.Linq;
using System.Text;
using Caravela.Framework.Aspects;
using Caravela.Framework.Code;
using Caravela.Framework.Code.Syntax;

namespace Caravela.Documentation.SampleCode.AspectFramework.ToString
{
    class ToStringAttribute : Attribute, IAspect<INamedType>
    {
        [Introduce( WhenExists = OverrideStrategy.Override, Name = "ToString" )]
        public string IntroducedToString()
        {
            var stringBuilder = InterpolatedStringBuilder.Create();
            stringBuilder.AddText("{ ");
            stringBuilder.AddText(meta.Target.Type.Name );
            stringBuilder.AddText(" ");

            var fields = meta.Target.Type.FieldsAndProperties.Where( f => !f.IsStatic ).ToList();

            var i = meta.CompileTime( 0 );

            foreach ( var field in fields)
            {
                if ( i > 0 )
                {
                    stringBuilder.AddText(", ");
                }

                stringBuilder.AddText(field.Name);
                stringBuilder.AddText("=");
                stringBuilder.AddExpression(field.Invokers.Final.GetValue(meta.This) );

                i++;
            }

            stringBuilder.AddText(" }");


            return stringBuilder.ToInterpolatedString();
            
        }
    }
}
