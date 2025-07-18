using Assets.Project.Code.Runtime.Gameplay.Common.LevelSystem;
using Assets.Project.Code.Runtime.Gameplay.Common.NPC;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Assets.Project.Code.Runtime.Gameplay.Common.SpwnerSystem
{
    public sealed partial class ActorsSpawnHandler : MonoBehaviour, IActorsSpawnHandler
    {
        [SerializeField]
        private List<ActorPoolConfig> actorPools;

        private Dictionary<ActorType, ObjectPool<ActorEntity>> pools = new();

        [SerializeField]
        private ActorSpawnPoint[] spawnPoint;

        public IReadOnlyList<ActorSpawnPoint> SpawnPoints => spawnPoint;

        private void Awake()
        {
            foreach (var config in actorPools)
            {
                var pool = new ObjectPool<ActorEntity>(
                    createFunc: () => Instantiate(config.Prefab, transform),
                    actionOnGet: actor => actor.gameObject.SetActive(true),
                    actionOnRelease: actor => actor.gameObject.SetActive(false),
                    actionOnDestroy: actor => Destroy(actor.gameObject),
                    collectionCheck: false,
                    defaultCapacity: config.DefaultCapacity,
                    maxSize: config.MaxSize
                );
                pools[config.ActorType] = pool;
            }
        }

        public ActorEntity Spawn(ActorType type, Quaternion rotation)
        {
            if (!pools.TryGetValue(type, out var pool))
                return null;

            var actor = pool.Get();
            var point = Random.Range(0, spawnPoint.Length);
            //actor.transform.SetPositionAndRotation(spawnPoint[point].GetPosition(), rotation);
            actor.transform.localPosition = spawnPoint[point].GetPosition();
            Debug.Log($"Spawning actor of type {type} at position {spawnPoint[point].GetPosition()}");
            actor.Initialize();

            LevelManager.Instance.VisitorsProvider.Incrementisitors();
            return actor;
        }

        public void Despawn(ActorType type, ActorEntity actor)
        {
            if (pools.TryGetValue(type, out var pool))
            {
                pool.Release(actor);
                LevelManager.Instance.VisitorsProvider.DecrementVisitors();
            }
        }
    }
}