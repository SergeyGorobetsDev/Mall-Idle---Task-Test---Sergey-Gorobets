using Assets.Project.Code.Runtime.Architecture.Engine;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Project.Code.Runtime.Gameplay.Common.WindowsSystem.Windows
{
    public sealed class MainMenuWindow : Window
    {
        [SerializeField]
        private Button startButton;
        [SerializeField]
        private Button settingsButton;
        [SerializeField]
        private Button exitButton;

        protected override void RegisterCallbacks()
        {
            base.RegisterCallbacks();
            startButton.onClick.AddListener(() =>
            {

                EngineSystem.Instance.WindowsNavigator.Show<LoadingWindow>();
                SceneManager.UnloadSceneAsync(1).completed += (operation) =>
                {
                };
                SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive).completed += (operation) =>
                {
                    EngineSystem.Instance.WindowsNavigator.Show<GameplayWindow>();
                };
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
            startButton.onClick.RemoveAllListeners();
            settingsButton.onClick.RemoveAllListeners();
            exitButton.onClick.RemoveAllListeners();
        }
    }
}