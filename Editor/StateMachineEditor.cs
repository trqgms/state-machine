using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using StateMachine.Abilities;
using StateMachine.Core;
using UnityEditor;
using UnityEngine;


namespace StateMachine
{
    [CustomEditor(typeof(StateMachineBase), true)]
    public class StateMachineEditor : Editor
    {
        private StateMachineBase Machine => (StateMachineBase) target;

        private Dictionary<Type, StateAbility> _abilities;
        private Dictionary<CharacterStateBase, IEnumerable<StateAbility>> _abilitiesForState;
        private Dictionary<Type, CharacterStateBase> _states;

        private Dictionary<Type, bool> _showAbilities;
        private Dictionary<Type, int> _showEachAbility; // bitmask!

        private CharacterStateBase _currentState;
        private Type _defaultState;

        //editor state
        private bool viewCustom;
        private bool viewStates;
        private Vector2 statesScroll;


        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (!Application.isPlaying) return;
            viewCustom = EditorGUILayout.Foldout(viewCustom, "Details");
            if (!viewCustom) return;

            GetField("_abilities", out _abilities);
            GetField("_states", out _states);
            GetField("_abilitiesForState", out _abilitiesForState);
            _showAbilities ??= _states.ToDictionary(p => p.Key, _ => false);
            _showEachAbility ??= _states.ToDictionary(p => p.Key, _ => 0);
            GetProperty("DefaultState", out _defaultState);
            GetProperty("CurrentState", out _currentState);
            if (_defaultState == null || _currentState == null) return;

            viewStates = EditorGUILayout.Foldout(viewStates, "States");
            using var statesScope = IndentationScope.Create();
            if (viewStates)
            {
                foreach (var (type, instance) in _states)
                {
                    DrawState(type, instance);
                }
            }
        }

        private void DrawState(Type type, CharacterStateBase instance)
        {
            var isCurrent = _currentState == instance;

            var abilitiesList = _abilitiesForState[instance].ToList();

            _showAbilities[type] =
                EditorGUILayout.Foldout(_showAbilities[type], isCurrent ? $"<<{type.Name}>>" : $"{type.Name}");
            using var stateScope = IndentationScope.Create();
            if (!abilitiesList.Any()) return;

            if (_showAbilities[type])
            {
                for (var i = 0; i < abilitiesList.Count; i++)
                {
                    var ability = abilitiesList[i];
                    var abilitiesMask = _showEachAbility[type];
                    var currentAbilityMask = 1 << i;
                    var display = (abilitiesMask & currentAbilityMask) == currentAbilityMask;


                    display = EditorGUILayout.Foldout(display, ability.GetType().Name);
                    using var abilityScope = IndentationScope.Create();


                    abilitiesMask |= currentAbilityMask;

                    if (display)
                    {
                        DrawAbility(ability);
                    }
                    else
                    {
                        abilitiesMask ^= currentAbilityMask;
                    }


                    _showEachAbility[type] = abilitiesMask;
                }
            }
        }


        private void DrawAbility(StateAbility ability)
        {
            GUILayout.Space(2);
            TypeDrawer.Draw(ability.GetType(), ability);
            GUILayout.Space(2);
        }

        private void GetProperty<T>(string propName, out T t) where T : class
        {
            var property = typeof(StateMachineBase).GetRuntimeProperties().First(fi => fi.Name == propName);
            t = property.GetValue(Machine) as T;
        }

        private void GetField<T>(string fieldName, out T t) where T : class
        {
            var fieldInfo = typeof(StateMachineBase).GetRuntimeFields().First(fi => fi.Name == fieldName);
            t = fieldInfo.GetValue(Machine) as T;
        }
    }
}