using System;

namespace StateMachine.Abilities.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class CollideWithAttribute : Attribute
    {
        public readonly Type[] otherAbilities;

        public CollideWithAttribute(params Type[] otherAbilities)
        {
            this.otherAbilities = otherAbilities;
        }
    }
}