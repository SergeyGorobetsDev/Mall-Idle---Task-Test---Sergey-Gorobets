using Assets.Project.Code.Runtime.Architecture.Engine;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Project.Code.Runtime.Gameplay.Common.WindowsSystem.Windows
{
    public sealed class PauseMenu : Window
    {
        [SerializeField]
        private Button continueButton;
        [SerializeField]
        private Button settingsButton;
        [SerializeField]
        private Button exitButton;

        protected override void RegisterCallbacks()
        {
            base.RegisterCallbacks();
            continueButton.onClick.AddListener(() =>
            {
                EngineSystem.Instance.WindowsNavigator.Pop();
                EngineSystem.Instance.WindowsNavigator.Show<GameplayWindow>();
            });

            settingsButton.onClick.AddListener(() =>
            {
                EngineSystem.Instance.WindowsNavigator.Show<SettingsWindow>();
            });

            exitButton.onClick.AddListener(() =>
            {
                EngineSystem.Instance.ApplicationQuit();
            });
        }

        protected override void UnRegisterCallbacks()
        {
            base.UnRegisterCallbacks();
            continueButton.onClick.RemoveAllListeners();
            settingsButton.onClick.RemoveAllListeners();
            exitButton.onClick.RemoveAllListeners();
        }
    }
}