using System;
using UnityEngine.AI;

namespace Assets.Project.Code.Runtime.Gameplay.Common.NPC
{
    [Serializable]
    public abstract class PayloadState<TPayload> : State, IPayloadState<TPayload>
    {
        public PayloadState(StateMachine actorStateMachine, ActorEntity actorEntity, NavMeshAgent navMeshAgent)
            : base(actorStateMachine, actorEntity, navMeshAgent) { }

        public abstract void Enter(TPayload payload);
    }
}