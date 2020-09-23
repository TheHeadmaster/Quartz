using System;
using System.Collections.Generic;
using System.Text;

namespace Quartz.IDE.ObjectModel.Attributes
{
    /// <summary>
    /// Signifies that a property, when changed, marks the object as unsaved.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class MementoAttribute : Attribute
    {
    }
}