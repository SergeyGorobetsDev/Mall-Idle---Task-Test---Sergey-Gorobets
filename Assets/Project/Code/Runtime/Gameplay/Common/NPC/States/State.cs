using System;
using UnityEngine.AI;

namespace Assets.Project.Code.Runtime.Gameplay.Common.NPC
{
    [Serializable]
    public class State
    {
        protected readonly StateMachine actorStateMachine;
        protected readonly ActorEntity actorEntity;
        protected NavMeshAgent navMeshAgent;

        public State(StateMachine actorStateMachine, ActorEntity actorEntity, NavMeshAgent navMeshAgent)
        {
            this.actorStateMachine = actorStateMachine;
            this.actorEntity = actorEntity;
            this.navMeshAgent = navMeshAgent;
        }

        public virtual void Enter()
        {
        }

        public virtual void Update()
        {
        }

        public virtual void Exit()
        {
        }

        protected bool TargetPositionReached()
        {
            if (!navMeshAgent.pathPending)
                if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
                    if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f)
                        return true;

            return false;
        }
    }
}