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
    public class GoToCashRegisterState : State
    {
        private Vector3 position;
        private InteriorEntity cashRegister;

        public GoToCashRegisterState(StateMachine actorStateMachine, ActorEntity actorEntity, NavMeshAgent navMeshAgent) : base(actorStateMachine, actorEntity, navMeshAgent)
        {
        }

        public override void Enter()
        {
            base.Enter();

            List<InteriorEntity> cashRegisters = LevelManager.Instance.InteriorObjectsHandler.GetInteriorObjects(InteriorType.CashRegister);
            if (cashRegisters.Count > 0)
            {
                cashRegister = cashRegisters[Random.Range(0, cashRegisters.Count)];
                cashRegister.InteriorQueue.PretendToBeInQueue();
                position = cashRegister.InteriorQueue.GetQueuePosition();
                navMeshAgent.SetDestination(position);
            }
            else
            {
                actorStateMachine.SetState<WaitForCachRegisterState>();
            }
        }

        public override void Update()
        {
            base.Update();

            if (cashRegister == null) return;

            if (TargetPositionReached())
                actorStateMachine.SetState<StayInPayFoodQueueState, InteriorEntity>(cashRegister);
            else
                navMeshAgent.SetDestination(position);
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}