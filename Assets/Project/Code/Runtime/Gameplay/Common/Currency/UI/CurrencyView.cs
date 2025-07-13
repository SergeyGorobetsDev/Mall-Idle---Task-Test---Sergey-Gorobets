using TMPro;
using UnityEngine;

namespace Assets.Project.Code.Runtime.Gameplay.Common.Currency.UI
{
    public sealed class CurrencyView : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text moneyText;

        private ICurrancyProvider currancyProvider;

        private void Awake()
        {
            if (moneyText == null)
            {
#if UNITY_EDITOR
                Debug.LogError("Money Text is not assigned in the CurrencyView component.");
#endif
                moneyText = this.GetComponentInChildren<TMP_Text>();
            }
        }

        public void Initalize(ICurrancyProvider currancyProvider)
        {
            this.currancyProvider = currancyProvider;
            UpdateMoneyView(currancyProvider.GetMoney());
            this.currancyProvider.OnMoneyChanged += UpdateMoneyView;
        }

        private void UpdateMoneyView(float value) =>
            moneyText.text = $"$ {value}";
    }
}
