using UnityEngine;

namespace Assets.Project.Code.Runtime.Gameplay.Common.WindowsSystem.Windows
{
    public class UpgradeView
    {
        [SerializeField]
        private UpgradeSlot[] upgradeSlots;

        [SerializeField]
        private UpgradeSlotView[] upgradeSlotViews;

        public void Initialize()
        {
            for (int i = 0; i < upgradeSlotViews.Length; i++)
                upgradeSlotViews[i].SetData(i);
        }

        public void Register()
        {
            for (int i = 0; i < upgradeSlotViews.Length; i++)
                upgradeSlotViews[i].OnUpgradeSlotClicked += UpgradeSlotClicke;
        }

        public void UnRegister()
        {
            for (int i = 0; i < upgradeSlotViews.Length; i++)
                upgradeSlotViews[i].OnUpgradeSlotClicked -= UpgradeSlotClicke;
        }

        private void UpgradeSlotClicke(int id)
        {

        }
    }
}