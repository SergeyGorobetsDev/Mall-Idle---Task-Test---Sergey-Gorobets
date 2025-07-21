using System;
using Zenject;

namespace Assets.Code.Scripts.Runtime.State_Machine
{
    public sealed class StatesFactory : IStatesFactory
    {
        private readonly DiContainer container;

        [Inject]
        public StatesFactory(DiContainer diContainer)
        {
            this.container = diContainer;
        }

        public T Create<T>() where T : State, new()
        {
            var state = container.TryResolve<T>();

            if (state == null)
            {
                Type type = typeof(T);
                state = new T();
                container.BindInterfacesAndSelfTo<T>().FromInstance(state).AsSingle();
            }

            return state;
        }
    }
}