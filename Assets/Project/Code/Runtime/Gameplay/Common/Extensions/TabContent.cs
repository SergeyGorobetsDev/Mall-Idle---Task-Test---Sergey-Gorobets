using Assets.Project.Code.Runtime.Gameplay.Common.WindowsSystem.Windows;
using UnityEngine;

namespace Assets.Project.Code.Runtime.Gameplay.Common.Extencions
{
    public class TabContent : MonoBehaviour
    {
        [SerializeField]
        protected UpgradeSlot[] upgradeSlots;

        public virtual void Initialize()
        {
            for (int i = 0; i < upgradeSlots.Length; i++)
                upgradeSlots[i].SetData(i);
        }

        public virtual void Show()
        {
            this.gameObject.SetActive(true);
            Register();
        }

        public virtual void Hide()
        {
            this.gameObject.SetActive(false);
            UnRegister();
        }

        protected virtual void Register()
        {
            for (int i = 0; i < upgradeSlots.Length; i++)
                upgradeSlots[i].OnUpgradeSlotClicked += UpgradeSlotClicked;

            Debug.Log("Registe Tab Slot");

        }

        protected virtual void UnRegister()
        {
            for (int i = 0; i < upgradeSlots.Length; i++)
                upgradeSlots[i].OnUpgradeSlotClicked -= UpgradeSlotClicked;

            Debug.Log("Unregiste Tab Slot");
        }

        protected virtual void UpgradeSlotClicked(int id)
        {
        }
    }
}
