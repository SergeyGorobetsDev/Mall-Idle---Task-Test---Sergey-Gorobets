using Assets.Project.Code.Runtime.Gameplay.Common.NPC;
using UnityEngine;

namespace Assets.Project.Code.Runtime.Gameplay.Common.SpwnerSystem
{
    public sealed partial class ActorsSpawnHandler
    {
        [System.Serializable]
        private class ActorPoolConfig
        {
            [SerializeField]
            public ActorType ActorType;
            [SerializeField]
            public ActorEntity Prefab;
            [SerializeField]
            public int DefaultCapacity = 5;
            [SerializeField]
            public int MaxSize = 20;
        }
    }
}