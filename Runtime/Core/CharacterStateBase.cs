using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace StateMachine.Core
{
    public abstract class CharacterStateBase
    {
        public Action GoBackAction { private get; set; }
        public Action<Type> MoveAction { private get; set; }
        public Func<Type, object> GetStateFunc { private get; set; }


        protected GameObject GameObject { get; private set; }

        protected void Move<T>() where T : CharacterStateBase
        {
            MoveAction?.Invoke(typeof(T));
        }

        protected void Quit()
        {
            GoBackAction?.Invoke();
        }

        protected T GetState<T>()
        {
            return (T) GetStateFunc?.Invoke(typeof(T));
        }

        protected void InitComponent<T>(out T t)
        {
            GameObject.TryGetComponent(out t);
        }

        protected void ChildComponent<T>(out T t)
        {
            t = GameObject.GetComponentInChildren<T>(true);
        }

        protected static void Find<T>(out T t) where T : Component
        {
            t = Object.FindObjectOfType<T>(true);
        }

        public void SetGameObject(GameObject gObj)
        {
            GameObject = gObj;
        }

        public virtual void OnCreated()
        {
        }

        public virtual void EnterState()
        {
        }

        public virtual void Update()
        {
        }

        public virtual void FixedUpdate()
        {
        }

        public virtual void ExitState()
        {
        }

        public virtual void Clear()
        {
        }
    }
}