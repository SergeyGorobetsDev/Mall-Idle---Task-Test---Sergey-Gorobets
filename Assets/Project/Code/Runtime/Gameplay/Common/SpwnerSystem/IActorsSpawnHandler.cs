using Assets.Project.Code.Runtime.Gameplay.Common.NPC;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Project.Code.Runtime.Gameplay.Common.SpwnerSystem
{
    public interface IActorsSpawnHandler
    {
        IReadOnlyList<ActorSpawnPoint> SpawnPoints { get; }
        void Despawn(ActorType type, ActorEntity actor);
        ActorEntity Spawn(ActorType type, Quaternion rotation);
    }
}