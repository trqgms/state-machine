using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Injector;

using StateMachine.Abilities;
using StateMachine.Abilities.Attributes;
using StateMachine.Core.Attributes;
using UnityEngine;

namespace StateMachine.Core
{
    public abstract class StateMachineBase : MonoBehaviour
    {
        private readonly Dictionary<Type, StateAbility> _abilities = new();

        private readonly Dictionary<CharacterStateBase, IEnumerable<StateAbility>> _abilitiesForState = new();

        private readonly Dictionary<Type, CharacterStateBase> _states = new();
        private Type _nextState;
        protected abstract Type DefaultState { get; }
        private CharacterStateBase CurrentState { get; set; }
        private IEnumerable<StateAbility> CurrentAbilities => _abilitiesForState[CurrentState];

        private void Start()
        {
            CreateAbilities();
            DeclareStates();
            InitializeDefaultState();
            ValidateAbilities();
            CurrentState.EnterState();
        }

        protected abstract void CreateAbilities();
        protected abstract void DeclareStates();

        private void InitializeDefaultState()
        {
            CurrentState = GetStateByType(DefaultState);
        }

        private void ValidateAbilities()
        {
#if UNITY_EDITOR
            if (_abilitiesForState.Validate(out var collisions))
            {
                return;
            }

            foreach (var (state, abilityA, abilityB) in collisions)
            {
                Debug.LogError(
                    $"on state {state.GetType().Name} the ability {abilityA.Name} collides with {abilityB.Name}");
            }

            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }

        private void Update()
        {
            CurrentState.Update();
            foreach (var ability in CurrentAbilities) ability.Update();

            TryPerformMovement();
        }

        private void FixedUpdate()
        {
            CurrentState.FixedUpdate();
            foreach (var ability in CurrentAbilities) ability.FixedUpdate();

            TryPerformMovement();
        }

        private void TryPerformMovement()
        {
            if (_nextState == null) return;

            var newState = GetStateByType(_nextState);
            _nextState = null;
            CurrentState.ExitState();
            foreach (var ability in CurrentAbilities) ability.Exit();

            CurrentState = newState;
            CurrentState.EnterState();
            foreach (var ability in CurrentAbilities) ability.Enter();
        }

        protected void DeclareAbility<T>(T instance) where T : StateAbility
        {
            _abilities[typeof(T)] = instance;
            instance.OnCreated();
        }

        protected CharacterStateBase CreateState<T>() where T : CharacterStateBase
        {
            Injection.Register<T>();
            var state = Injection.Get<T>();
            Injection.Clear<T>();

            _states[typeof(T)] = state;
            state.MoveAction = GoToState;
            state.GoBackAction = () => GoToState(DefaultState);
            state.GetStateFunc = GetStateByType;
            state.SetGameObject(gameObject);
            state.OnCreated();

            _abilitiesForState[state] = typeof(T)
                .GetCustomAttributes<AllowAbilitiesAttribute>()
                .SelectMany(a => a.abilities)
                .Select(a => _abilities[a]);

            return state;
        }

        private CharacterStateBase GetStateByType(Type type)
        {
            const string nullMessage = "getting state with null as type!";

            if (type != null && _states.TryGetValue(type, out var state)) return state;

            Debug.LogError(type == null ? nullMessage : $"No state registered of type {type.Name}");
            return null;
        }

        public T GetState<T>() where T : CharacterStateBase
        {
            if (_states.TryGetValue(typeof(T), out var state)) return state as T;

            Debug.LogError($"there is not state of type {typeof(T).Name}");
            return null;
        }
        
        private StateAbility GetAbilityByType(Type type)
        {
            const string nullMessage = "getting ability with null as type!";

            if (type != null && _abilities.TryGetValue(type, out var ability)) return ability;

            Debug.LogError(type == null ? nullMessage : $"No ability registered of type {type.Name}");
            return null;
        }

        public T GetAbility<T>() where T : StateAbility
        {
            if (_abilities.TryGetValue(typeof(T), out var ability)) return ability as T;

            Debug.LogError($"there is not ability of type {typeof(T).Name}");
            return null;
        }

        private void GoToState(Type stateType)
        {
            _nextState = stateType;
        }
    }
}