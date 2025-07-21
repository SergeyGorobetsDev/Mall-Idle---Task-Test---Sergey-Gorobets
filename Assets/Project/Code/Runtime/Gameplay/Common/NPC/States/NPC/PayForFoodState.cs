using Assets.Project.Code.Runtime.Gameplay.Common.InteriorSystem;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Project.Code.Runtime.Gameplay.Common.NPC
{
    [Serializable]
    public class PayForFoodState : PayloadState<InteriorEntity>
    {
        private InteriorEntity cashRegister;
        private float waitTime;

        public PayForFoodState(StateMachine actorStateMachine, ActorEntity actorEntity, NavMeshAgent navMeshAgent) : base(actorStateMachine, actorEntity, navMeshAgent)
        {
        }

        public override void Enter(InteriorEntity cashRegister)
        {
            base.Enter();

            this.cashRegister = cashRegister;
            waitTime = cashRegister.CurrentUpgradeData.Value;
            cashRegister.Procces();
        }

        public override void Update()
        {
            base.Update();

            waitTime -= Time.deltaTime;
            if (waitTime <= 0f)
            {
                actorStateMachine.SetState<GoOutState>();
            }
        }

        public override void Exit()
        {
            base.Exit();
            cashRegister.InteriorQueue.LeaveQueue(actorEntity);
        }
    }
}