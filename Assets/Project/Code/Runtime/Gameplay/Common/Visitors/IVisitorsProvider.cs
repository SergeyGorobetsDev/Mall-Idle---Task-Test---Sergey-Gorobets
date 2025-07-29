using System;

namespace Assets.Project.Code.Runtime.Gameplay.Common.LevelSystem
{
    public interface IVisitorsProvider
    {
        int CurrentVisitors { get; }
        int MaxVisitors { get; }

        event Action<int> OnVisitorsChanged;

        void IncrementMaxVisitors(int amount = 1);
        void Incrementisitors();
        void DecrementVisitors();
        void Tick();
        void Initialize(int maxVisitors);
    }
}