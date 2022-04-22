using UnityEditor;
using UnityEngine;

namespace StateMachine
{
    public class IndentationScope : GUI.Scope
    {
        public static IndentationScope Create() => new();

        private IndentationScope()
        {
            EditorGUI.indentLevel++;
        }

        protected override void CloseScope()
        {
            EditorGUI.indentLevel--;
        }
    }
}