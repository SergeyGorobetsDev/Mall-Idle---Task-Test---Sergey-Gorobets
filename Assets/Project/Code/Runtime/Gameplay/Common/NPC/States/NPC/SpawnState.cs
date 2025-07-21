using UnityEngine;
using UnityEngine.AI;

namespace Assets.Project.Code.Runtime.Gameplay.Common.NPC
{
    public sealed class SpawnState : State
    {
        private const float waitDefaultTimer = 2f;
        private float waitTimer;

        public SpawnState(StateMachine actorStateMachine, ActorEntity actorEntity, NavMeshAgent navMeshAgent) : base(actorStateMachine, actorEntity, navMeshAgent)
        {
        }

        public override void Enter()
        {
            base.Enter();
            waitTimer = waitDefaultTimer;
        }

        public override void Update()
        {
            waitTimer -= Time.deltaTime;
            if (waitTimer <= 0f)
            {
                navMeshAgent.enabled = true;
                actorStateMachine.SetState<GetFoodState>();
            }
        }
    }
}
