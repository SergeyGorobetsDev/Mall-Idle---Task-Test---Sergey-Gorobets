using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Project.Code.Runtime.Gameplay.Common.NPC
{
    [Serializable]
    public class StateMachine
    {
        [SerializeField, SerializeReference]
        private List<State> states = new(7);

        [SerializeField]
        private State activeState;

        public void Initialize(ActorEntity actorEntity, NavMeshAgent agent)
        {
            states.Clear();
            var spawnState = new SpawnState(this, actorEntity, agent);
            var waitState = new WaitForCachRegisterState(this, actorEntity, agent);
            var getFoodState = new GetFoodState(this, actorEntity, agent);
            var payForFoodState = new PayForFoodState(this, actorEntity, agent);
            var goOutState = new GoOutState(this, actorEntity, agent);
            var despawnState = new DespawnState(this, actorEntity, agent);
            var stayInGetFoodQueueState = new StayInFoodQueueState(this, actorEntity, agent);
            var goToCashRegisterState = new GoToCashRegisterState(this, actorEntity, agent);
            var stayInPayFoodQueueState = new StayInPayFoodQueueState(this, actorEntity, agent);

            states = new List<State>(8)
            {
                spawnState,
                waitState,
                getFoodState,
                payForFoodState,
                goOutState,
                despawnState,
                stayInGetFoodQueueState,
                goToCashRegisterState,
                stayInPayFoodQueueState
            };
        }

        public void SetState<TState>() where TState : State =>
            SetState(typeof(TState));

        public void SetState<TState, TPayload>(TPayload payload) where TState : State, IPayloadState<TPayload>
        {
            activeState?.Exit();
            var state = (IPayloadState<TPayload>)GetState(typeof(TState));
            activeState = (State)state;
            state.Enter(payload);
        }

        public void Update()
        {
            activeState?.Update();
        }

        private State GetState(Type type)
        {
            foreach (State state in states)
                if (state.GetType() == type)
                    return state;

            throw new Exception($"The <{type.Name}> is not found!");
        }

        private void SetState(Type type)
        {
            activeState?.Exit();
            activeState = GetState(type);
            activeState.Enter();
        }
    }
}