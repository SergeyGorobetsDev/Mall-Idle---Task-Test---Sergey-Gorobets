using Assets.Project.Code.Runtime.Gameplay.Common.InteriorSystem;
using System;
using UnityEngine.AI;

namespace Assets.Project.Code.Runtime.Gameplay.Common.NPC
{
    [Serializable]
    public class StayInFoodQueueState : PayloadState<InteriorEntity>
    {
        private InteriorEntity target;
        private InteriorQueuePoint InteriorQueuePoint;

        public StayInFoodQueueState(StateMachine actorStateMachine, ActorEntity actorEntity, NavMeshAgent navMeshAgent) : base(actorStateMachine, actorEntity, navMeshAgent)
        {
        }

        public override void Enter(InteriorEntity target)
        {
            this.target = target;
            this.InteriorQueuePoint = target.InteriorQueue;
            this.InteriorQueuePoint.JoinQueue(actorEntity);
        }

        public override void Update()
        {
            base.Update();

            if (InteriorQueuePoint.IsFirstInQueue(actorEntity))
                actorStateMachine.SetState<GoToCashRegisterState>();
        }

        public override void Exit()
        {
            base.Exit();
            this.InteriorQueuePoint.LeaveQueue(actorEntity);
        }
    }
}