using Assets.Project.Code.Runtime.Gameplay.Common.InteriorSystem;
using Assets.Project.Code.Runtime.Gameplay.Common.LevelSystem;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Assets.Project.Code.Runtime.Gameplay.Common.NPC
{
    [Serializable]
    public class GetFoodState : State
    {
        private Vector3 position;
        private InteriorEntity freezer;

        public GetFoodState(StateMachine actorStateMachine, ActorEntity actorEntity, NavMeshAgent navMeshAgent) : base(actorStateMachine, actorEntity, navMeshAgent)
        {
        }

        public override void Enter()
        {
            base.Enter();
            List<InteriorEntity> freezers = LevelManager.Instance.InteriorObjectsHandler.GetInteriorObjects(InteriorType.Freezer);
            if (freezers.Count > 0)
            {
                freezer = freezers[Random.Range(0, freezers.Count)];
                freezer.InteriorQueue.PretendToBeInQueue();
                position = freezer.InteriorQueue.GetQueuePosition();
                navMeshAgent.SetDestination(position);
            }
            else
            {
                Debug.LogWarning("No freezers available for the actor to get food.");
            }
        }

        public override void Update()
        {
            base.Update();

            if (position == null) return;

            if (TargetPositionReached())
                actorStateMachine.SetState<StayInFoodQueueState, InteriorEntity>(freezer);
            else navMeshAgent.SetDestination(position);
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}