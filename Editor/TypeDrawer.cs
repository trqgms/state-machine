using System;
using System.Reflection;
using UnityEditor;

namespace StateMachine
{
    public static class TypeDrawer
    {
        private static Type _type;
        private static object _instance;

        public static void Draw(Type type, object instance)
        {
            _type = type;
            _instance = instance;

            if (type == null) return;

            foreach (var field in _type.GetRuntimeFields())
            {
                DrawField(field);
            }
        }

        private static void DrawField(FieldInfo field)
        {
            var fieldType = field.FieldType;
            var isAsset = typeof(UnityEngine.Object).IsAssignableFrom(fieldType);
            if (isAsset)
            {
                EditorGUILayout.ObjectField(field.Name, field.GetValue(_instance) as UnityEngine.Object, fieldType,
                    false);
            }
            else
            {
                EditorGUILayout.TextField(field.Name + $" : {fieldType.Name}",
                    field.GetValue(_instance)?.ToString() ?? "null");
            }
        }
    }
}