using TMPro;
using UnityEngine;

namespace Assets.Project.Code.Runtime.Gameplay.Common.LevelSystem
{
    public sealed class VisitorsView : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text visitorsAmountText;

        private IVisitorsProvider visitorsProvider;

        private void Awake()
        {
            InitComponents();
        }

        private void InitComponents()
        {
            if (visitorsAmountText == null)
                visitorsAmountText = this.GetComponentInChildren<TMP_Text>();
        }

        public void Initalize(IVisitorsProvider visitorsProvider)
        {
            this.visitorsProvider = visitorsProvider;
            UpdateVisitorsView(visitorsProvider.CurrentVisitors);
            this.visitorsProvider.OnVisitorsChanged += UpdateVisitorsView;
        }

        private void UpdateVisitorsView(int value) =>
            visitorsAmountText.text = $" {value}";
    }
}