using Assets.Project.Code.Runtime.Gameplay.Common.InteriorSystem;
using System;
using UnityEngine.AI;

namespace Assets.Project.Code.Runtime.Gameplay.Common.NPC
{
    [Serializable]
    public class StayInPayFoodQueueState : PayloadState<InteriorEntity>
    {
        private InteriorEntity target;
        private InteriorQueuePoint interiorQueuePoint;

        public StayInPayFoodQueueState(StateMachine actorStateMachine, ActorEntity actorEntity, NavMeshAgent navMeshAgent) : base(actorStateMachine, actorEntity, navMeshAgent)
        {
        }

        public override void Enter(InteriorEntity target)
        {
            this.target = target;
            this.interiorQueuePoint = target.InteriorQueue;
            this.interiorQueuePoint.JoinQueue(actorEntity);
        }

        public override void Update()
        {
            base.Update();

            if (interiorQueuePoint.IsFirstInQueue(actorEntity))
                actorStateMachine.SetState<PayForFoodState, InteriorEntity>(target);
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}