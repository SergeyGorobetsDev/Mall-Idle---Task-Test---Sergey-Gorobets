using Assets.Project.Code.Runtime.Gameplay.Common.NPC;
using System;
using UnityEngine;

namespace Assets.Project.Code.Runtime.Gameplay.Common.SpwnerSystem
{
    [Serializable]
    public sealed class ActorPoolConfig
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