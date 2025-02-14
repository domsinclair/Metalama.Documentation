---
uid: child-aspects
---

# Adding child aspects

An aspect can add other aspects to children declarations. Such aspects are called _child aspects_. Child aspects must satisfy two conditions:

* The child aspect class must be processed _after_ the parent aspect class, i.e. the child aspect class must be positioned _before_ the parent class in the <xref:Caravela.Framework.Aspects.AspectOrderAttribute> ordering definition. See <xref:ordering-aspects> for details.
* The target declaration of the child aspect class must be contained in the target type of the parent aspect. For instance, a type-level aspect can add aspects to methods of the current type, but not to methods of a different type. A parameter-level aspect can add a child aspect to the parent method or to another method of the same type, but not of a different type.

Parent aspects are listed in the <xref:Caravela.Framework.Aspects.IAspectInstance.Predecessors?text=IAspectInstance.Predecessors> property, which the child aspect can access from <xref:Caravela.Framework.Aspects.meta.AspectInstance?text=meta.AspectInstance> or <xref:Caravela.Framework.Aspects.IAspectLayerBuilder.AspectInstance?text=builder.AspectInstance>.

> TODO: Example
