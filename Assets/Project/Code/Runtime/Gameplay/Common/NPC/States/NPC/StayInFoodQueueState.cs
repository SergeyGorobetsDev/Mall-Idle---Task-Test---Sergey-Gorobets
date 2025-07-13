using Assets.Project.Code.Runtime.Gameplay.Common.InteriorSystem;
using System;
using UnityEngine;
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
            Debug.Log($"Entering {nameof(StayInFoodQueueState)}");

            this.target = target;
            this.InteriorQueuePoint = target.InteriorQueue;
            this.InteriorQueuePoint.JoinQueue(actorEntity);
            Debug.Log($"Entering {nameof(StayInPayFoodQueueState)} with target: {target}");
        }

        public override void Update()
        {
            base.Update();

            if (InteriorQueuePoint.IsFirstInQueue(actorEntity))
            {
                Debug.Log($"{actorEntity} is first in queue at {InteriorQueuePoint.name}. Transitioning to GoToCashRegisterState.");
                target.Procces();
                actorStateMachine.SetState<GoToCashRegisterState>();
            }
            else
            {
                Debug.Log($"{actorEntity} is not first in queue at {InteriorQueuePoint.name}. Waiting...");
            }
        }

        public override void Exit()
        {
            base.Exit();
            this.InteriorQueuePoint.LeaveQueue(actorEntity);
        }
    }
}