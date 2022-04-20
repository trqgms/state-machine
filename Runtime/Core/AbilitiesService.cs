using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using StateMachine.Abilities;
using StateMachine.Abilities.Attributes;

namespace StateMachine.Core
{
    public static class AbilitiesService
    {
        public static bool Validate(this IReadOnlyDictionary<CharacterStateBase, IEnumerable<StateAbility>> abilitiesForState, out IEnumerable<(CharacterStateBase state, Type abilityA, Type abilityB)> collisions)
        {
            collisions = abilitiesForState.SelectMany(GetCollisionsOnPair);
            return !collisions.Any();
        }

        private static IEnumerable<(CharacterStateBase, Type, Type)> GetCollisionsOnPair(
            KeyValuePair<CharacterStateBase, IEnumerable<StateAbility>> pair)
        {
            return GetCollisionsTuples(pair.Key, pair.Value);
        }

        private static IEnumerable<(CharacterStateBase Key, Type ability, Type)> GetCollisionsTuples(
            CharacterStateBase ability, IEnumerable<StateAbility> stateAbilities)
        {
            var abilityType = ability.GetType();
            return CheckAbilityForCollisions(abilityType, stateAbilities).Select(t => (ability, a: abilityType, b: t));
        }

        private static IEnumerable<Type> CheckAbilityForCollisions(Type abType, IEnumerable<StateAbility> abilities)
        {
            return abilities.Where(IsIncompatible(abType)).Select(a => a.GetType());
        }

        private static Func<StateAbility, bool> IsIncompatible(Type type) => ability => type
            .GetIncompatibleAbilities()
            .Contains(ability.GetType());

        private static IEnumerable<Type> GetIncompatibleAbilities(this Type type) => type
            .GetCustomAttributes<CollideWithAttribute>()
            .SelectMany(c => c.otherAbilities);
    }
}