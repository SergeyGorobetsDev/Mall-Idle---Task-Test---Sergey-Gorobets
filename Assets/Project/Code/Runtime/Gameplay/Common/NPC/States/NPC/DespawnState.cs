using Assets.Project.Code.Runtime.Gameplay.Common.LevelSystem;
using System;
using UnityEngine.AI;

namespace Assets.Project.Code.Runtime.Gameplay.Common.NPC
{
    [Serializable]
    public class DespawnState : State
    {
        public DespawnState(StateMachine actorStateMachine, ActorEntity actorEntity, NavMeshAgent agent) : base(actorStateMachine, actorEntity, agent)
        {
        }

        public override void Enter()
        {
            base.Enter();
            LevelManager.Instance.ActorsSpawnHandler.Despawn(ActorType.Buyer, actorEntity);
        }
    }
}