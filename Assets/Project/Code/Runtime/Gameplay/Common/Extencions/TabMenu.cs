using UnityEngine;

namespace Assets.Project.Code.Runtime.Gameplay.Common.Extencions
{
    public sealed class TabMenu : MonoBehaviour
    {
        [SerializeField]
        private Tab[] tabs;

        [SerializeField]
        private TabContent[] tabContents;

        private int activeId = -1;
        private bool isRegistered = false;
        public void Initialize()
        {
            for (int i = 0; i < tabs.Length; i++)
                tabs[i].SetData(i);

            foreach (TabContent content in tabContents)
                content.Hide();

            ActivateFirsTab();
        }

        public void Register()
        {
            if (isRegistered) return;

            foreach (Tab tab in tabs)
                tab.OnTabSelected += OnTabSelected;

            isRegistered = true;
        }

        public void UnRegister()
        {
            foreach (Tab tab in tabs)
                tab.OnTabSelected -= OnTabSelected;

            isRegistered = false;
        }

        private void OnTabSelected(int id)
        {
            if (activeId > -1)
            {
                tabContents[activeId].Hide();
                tabs[activeId].ChangeState(false);
            }

            tabContents[id].Initialize();
            tabContents[id].Show();
            tabs[id].ChangeState(true);
            activeId = id;
        }

        private void ActivateFirsTab()
        {
            OnTabSelected(0);
        }
    }
}