using System;

namespace StateMachine.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class AllowAbilitiesAttribute : Attribute
    {
        public readonly Type[] abilities;

        public AllowAbilitiesAttribute(params Type[] abilities)
        {
            this.abilities = abilities;
        }
    }
}