using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Project.Code.Runtime.Gameplay.Common.WindowsSystem.Windows
{
    public class UpgradeSlotView : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField]
        private int slotId;

        public event Action<int> OnUpgradeSlotClicked;

        public void SetData(int id)
        {
            slotId = id;
        }

        public void OnPointerClick(PointerEventData eventData) =>
            OnUpgradeSlotClicked?.Invoke(slotId);
    }
}