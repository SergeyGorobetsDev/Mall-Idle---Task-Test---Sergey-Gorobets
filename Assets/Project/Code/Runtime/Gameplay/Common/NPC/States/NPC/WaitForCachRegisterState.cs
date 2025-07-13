using UnityEngine;
using UnityEngine.AI;

namespace Assets.Project.Code.Runtime.Gameplay.Common.NPC
{
    [SerializeField]
    public class WaitForCachRegisterState : State
    {
        private float waitDefaultTimer = 5f;
        private float waitTimer;

        public WaitForCachRegisterState(StateMachine actorStateMachine, ActorEntity actorEntity, NavMeshAgent navMeshAgent) : base(actorStateMachine, actorEntity, navMeshAgent)
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
                actorStateMachine.SetState<GoToCashRegisterState>();
        }
    }
}