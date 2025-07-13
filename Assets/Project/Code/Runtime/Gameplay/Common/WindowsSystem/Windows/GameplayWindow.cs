using Assets.Project.Code.Runtime.Architecture.Engine;
using Assets.Project.Code.Runtime.Gameplay.Common.Currency.UI;
using Assets.Project.Code.Runtime.Gameplay.Common.Extencions;
using Assets.Project.Code.Runtime.Gameplay.Common.LevelSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Project.Code.Runtime.Gameplay.Common.WindowsSystem.Windows
{
    public class GameplayWindow : Window
    {
        [SerializeField]
        private CurrencyView currencyView;

        [SerializeField]
        private Button settingsButton;

        [SerializeField]
        private Button pauseButton;

        [SerializeField]
        private TabMenu upgradesTabMenu;

        public override void Show()
        {
            base.Show();
            upgradesTabMenu.Initialize();
        }

        protected override void BindDocumentData()
        {
            base.BindDocumentData();

            currencyView.Initalize(LevelManager.Instance.CurrencyProvider);
            upgradesTabMenu.Initialize();
        }

        protected override void RegisterCallbacks()
        {
            base.RegisterCallbacks();
            settingsButton.onClick.AddListener(() =>
            {
                EngineSystem.Instance.WindowsNavigator.Show<SettingsWindow>();
            });

            pauseButton.onClick.AddListener(() =>
            {
                EngineSystem.Instance.WindowsNavigator.Show<PauseMenu>();
            });

            upgradesTabMenu.Register();
        }

        protected override void UnRegisterCallbacks()
        {
            settingsButton.onClick.RemoveAllListeners();
            pauseButton.onClick.RemoveAllListeners();
            upgradesTabMenu.UnRegister();
            base.UnRegisterCallbacks();
        }
    }
}