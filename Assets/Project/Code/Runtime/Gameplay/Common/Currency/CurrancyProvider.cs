using Assets.Project.Code.Runtime.Architecture.Engine;
using System;
using UnityEngine;

namespace Assets.Project.Code.Runtime.Gameplay.Common.Currency
{
    [Serializable]
    public sealed class CurrancyProvider : ICurrancyProvider
    {
        [SerializeField]
        private float money;

        public event Action<float> OnMoneyChanged;

        public void Initialize(float initialMoney)
        {
            if (initialMoney < 0)
            {
#if UNITY_EDITOR
                Debug.LogWarning("Initial money cannot be negative. Setting to 0.");
#endif
                initialMoney = 0;
            }
            money = initialMoney;
            OnMoneyChanged?.Invoke(money);
#if UNITY_EDITOR
            Debug.Log($"CurrencyProvider initialized with {money} money.");
#endif
        }

        public float GetMoney() => money;

        public void AddMoney(float amount)
        {
            if (amount < 0)
            {
#if UNITY_EDITOR
                Debug.LogWarning("Cannot add negative money.");
#endif
                return;
            }
            money += amount;
            EngineSystem.Instance.SaveLoadHandler.ProgressData.Money = money;
            OnMoneyChanged?.Invoke(money);
#if UNITY_EDITOR
            Debug.Log($"Added {amount} money. Total: {money}");
#endif
        }

        public void RemoveMoney(float amount)
        {
            if (amount < 0)
            {
#if UNITY_EDITOR
                Debug.LogWarning("Cannot remove negative money.");
#endif
                return;
            }
            if (money < amount)
            {
#if UNITY_EDITOR
                Debug.LogWarning("Not enough money to remove.");
#endif
                return;
            }
            money -= amount;
            EngineSystem.Instance.SaveLoadHandler.ProgressData.Money = money;
            OnMoneyChanged?.Invoke(money);
#if UNITY_EDITOR
            Debug.Log($"Removed {amount} money. Total: {money}");
#endif
        }
    }
}