using System;

namespace Assets.Project.Code.Runtime.Gameplay.Common.Currency
{
    public interface ICurrancyProvider
    {
        event Action<float> OnMoneyChanged;

        void AddMoney(float amount);
        float GetMoney();
        void Initialize(float initialMoney);
        void RemoveMoney(float amount);
    }
}