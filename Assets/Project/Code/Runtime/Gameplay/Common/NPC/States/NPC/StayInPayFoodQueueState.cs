using Assets.Project.Code.Runtime.Gameplay.Common.InteriorSystem;
using System;
using UnityEngine;
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
            Debug.Log($"Entering {nameof(StayInPayFoodQueueState)}");

            this.target = target;
            this.interiorQueuePoint = target.InteriorQueue;
            this.interiorQueuePoint.JoinQueue(actorEntity);
        }

        public override void Update()
        {
            base.Update();


            if (interiorQueuePoint.IsFirstInQueue(actorEntity))
            {
                actorStateMachine.SetState<PayForFoodState, InteriorEntity>(target);
            }
            else
            {
                Debug.Log($"{actorEntity} is not first in queue at {interiorQueuePoint.name}. Waiting...");
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}