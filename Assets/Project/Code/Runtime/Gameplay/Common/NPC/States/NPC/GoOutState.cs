using Assets.Project.Code.Runtime.Gameplay.Common.LevelSystem;
using Assets.Project.Code.Runtime.Gameplay.Common.SpwnerSystem;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Assets.Project.Code.Runtime.Gameplay.Common.NPC
{
    [Serializable]
    public class GoOutState : State
    {
        private Vector3 position;

        public GoOutState(StateMachine actorStateMachine, ActorEntity actorEntity, NavMeshAgent agent) : base(actorStateMachine, actorEntity, agent)
        {
        }

        public override void Enter()
        {
            base.Enter();

            Debug.Log($"Entering {nameof(GoOutState)}");

            navMeshAgent.isStopped = false;
            IReadOnlyList<ActorSpawnPoint> points = LevelManager.Instance.ActorsSpawnHandler.SpawnPoints;
            position = points[Random.Range(0, points.Count)].GetPosition();
            navMeshAgent.SetDestination(position);
        }

        public override void Update()
        {
            if (TargetPositionReached())
                actorStateMachine.SetState<DespawnState>();
            else navMeshAgent.SetDestination(position);
        }
    }
}