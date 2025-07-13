using Assets.Project.Code.Runtime.Architecture.Engine;
using System;
using UnityEngine;

namespace Assets.Project.Code.Runtime.Gameplay.Common.LevelSystem
{
    [Serializable]
    public sealed class VisitorsProvider : IVisitorsProvider
    {
        [SerializeField]
        private int currentVisitors = 0;
        [SerializeField]
        private int maxVisitors = 0;

        public int CurrentVisitors => currentVisitors;
        public int MaxVisitors => maxVisitors;

        public void Initialize(int maxVisitors) => this.maxVisitors = maxVisitors;

        public void IncrementMaxVisitors(int amount = 1)
        {
            maxVisitors += amount;
            EngineSystem.Instance.SaveLoadHandler.ProgressData.AvailableVisitorAmoung = maxVisitors;
        }


        public void Incrementisitors()
        {
            if (currentVisitors < maxVisitors)
                currentVisitors++;
        }

        public void DecrementVisitors()
        {
            if (currentVisitors > 1)
                currentVisitors--;
        }

        public void Tick()
        {
            if (currentVisitors < maxVisitors)
            {
                currentVisitors++;
                LevelManager.Instance.ActorsSpawnHandler.Spawn(NPC.ActorType.Buyer, Quaternion.identity);
            }
            else
            {
#if UNITY_EDITOR
                Debug.LogWarning("Max visitors limit reached.");
#endif
            }
        }
    }
}