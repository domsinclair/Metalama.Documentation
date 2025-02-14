﻿using System;
using System.Linq;
using Caravela.Framework.Aspects;
using Caravela.Framework.Code;

namespace Caravela.Documentation.SampleCode.AspectFramework.DeepClone
{
    public class DeepCloneAttribute : TypeAspect
    {
        public override void BuildAspectClass(IAspectClassBuilder builder)
        {
            builder.IsInherited = true;
        }

        public override void BuildAspect(IAspectBuilder<INamedType> builder)
        {
            var typedMethod = builder.Advices.IntroduceMethod(
                builder.Target,
                nameof(CloneImpl),
                whenExists: OverrideStrategy.Override);

            typedMethod.Name = "Clone";
            typedMethod.ReturnType = builder.Target;

            builder.Advices.ImplementInterface(
                builder.Target,
                typeof(ICloneable),
                whenExists: OverrideStrategy.Ignore);
        }

        [Template(IsVirtual = true)]
        public virtual dynamic CloneImpl()
        {
            // This compile-time variable will receive the expression representing the base call.
            // If we have a public Clone method, we will use it (this is the chaining pattern). Otherwise,
            // we will call MemberwiseClone (this is the initialization of the pattern).
            IExpression baseCall;

            if (meta.Target.Method.IsOverride)
            {
                meta.DefineExpression(meta.Base.Clone(), out baseCall);
            }
            else
            {
                meta.DefineExpression(meta.Base.MemberwiseClone(), out baseCall);
            }

            // Define a local variable of the same type as the target type.
            var clone = meta.Cast(meta.Target.Type, baseCall)!;

            // Select clonable fields.
            var clonableFields =
                meta.Target.Type.FieldsAndProperties.Where(
                    f => f.IsAutoPropertyOrField &&
                    ((f.Type.Is(typeof(ICloneable)) && f.Type.SpecialType != SpecialType.String) ||
                    (f.Type is INamedType fieldNamedType && fieldNamedType.Aspects<DeepCloneAttribute>().Any())));

            foreach (var field in clonableFields)
            {
                // Check if we have a public method 'Clone()' for the type of the field.
                var fieldType = (INamedType)field.Type;
                var cloneMethod = fieldType.Methods.OfExactSignature("Clone", Array.Empty<IType>());

                if (cloneMethod is { Accessibility: Accessibility.Public } ||
                     fieldType.Aspects<DeepCloneAttribute>().Any())
                {
                    // If yes, call the method without a cast.
                    field.Invokers.Base.SetValue(
                        clone,
                        meta.Cast(fieldType, field.Invokers.Base.GetValue(meta.This).Clone()));

                }
                else
                {
                    // If no, use the interface.
                    field.Invokers.Base.SetValue(
                        clone,
                        meta.Cast(fieldType, ((ICloneable)field.Invokers.Base.GetValue(meta.This)).Clone()));
                }
            }

            return clone;
        }

        [InterfaceMember(IsExplicit = true)]
        object Clone() => meta.This.Clone();

    }
}
