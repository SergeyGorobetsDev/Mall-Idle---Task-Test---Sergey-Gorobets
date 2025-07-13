using Assets.Project.Code.Runtime.Architecture.Resources_System;
using Assets.Project.Code.Runtime.Gameplay.Common.AudioSystem;
using Assets.Project.Code.Runtime.Gameplay.Common.Camera_System;
using Assets.Project.Code.Runtime.Gameplay.Common.WindowsSystem;
using Assets.Project.Code.Runtime.Gameplay.Common.WindowsSystem.Windows;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Project.Code.Runtime.Architecture.Engine
{
    /// <summary>
    /// ДЛЯ ПРОВЕРЯЮЩЕГО
    /// Класс используется как контейнер, так как Zenject был запрещен для использования в проекте.
    /// В данном классе реализованы глобальные системы, которые будут доступны в любой части игры.
    /// Класс живет всю игру, поэтому он помечен как DontDestroyOnLoad.
    /// В нем реализована инициализация глобальных систем, таких как AudioPlayer и SaveLoadHandler и т.д
    /// Некоторые решения могут быть спорными для продакшна, но для быстроты выполнения ТЗ, мною были приняты такие решения.
    /// </summary>
    public class EngineSystem : MonoBehaviour
    {
        [Header("Global Systems")]
        [SerializeField]
        private AudioPlayer audioPlayer;
        [SerializeField]
        private SaveLoadHandler saveLoadHandler;
        [SerializeField]
        private ResourcesProvider resourcesProvider;
        [SerializeField]
        private WindowsNavigator windowsNavigator;

        [Header("Providers")]
        [SerializeField]
        private CameraProvider cameraProvider;

        public IAudioPlayer AudioPlayer
        {
            get => audioPlayer;
            set => audioPlayer = value as AudioPlayer;
        }
        public ISaveLoadHandler SaveLoadHandler
        {
            get => saveLoadHandler;
            set => saveLoadHandler = value as SaveLoadHandler;
        }
        public IResourcesProvider ResourcesProvider
        {
            get => resourcesProvider;
            set => resourcesProvider = value as ResourcesProvider;
        }
        public ICameraProvider CameraProvider
        {
            get => cameraProvider;
            set => cameraProvider = value as CameraProvider;
        }
        public IWindowsNavigator WindowsNavigator
        {
            get => windowsNavigator;
            set => windowsNavigator = value as WindowsNavigator;
        }

        public static EngineSystem Instance { get; private set; }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
            Initialze();
        }

        public async void Initialze()
        {
            if (audioPlayer == null)
            {
                audioPlayer = GetComponentInChildren<AudioPlayer>();
                if (audioPlayer == null)
                {
#if UNITY_EDITOR
                    Debug.LogError("AudioPlayer not found in the scene.");
#endif
                    return;
                }
            }

            if (saveLoadHandler == null)
            {
                saveLoadHandler = GetComponentInChildren<SaveLoadHandler>();
                if (saveLoadHandler == null)
                {
#if UNITY_EDITOR
                    Debug.LogError("SaveLoadHandler not found in the scene.");
#endif
                    return;
                }
            }

            if (resourcesProvider == null)
            {
                resourcesProvider = GetComponentInChildren<ResourcesProvider>();
                if (resourcesProvider == null)
                {
#if UNITY_EDITOR
                    Debug.LogError("ResourcesProvider not found in the scene.");
#endif
                    return;
                }
            }

            if (windowsNavigator == null)
            {
                windowsNavigator = GetComponentInChildren<WindowsNavigator>();
                if (windowsNavigator == null)
                {
#if UNITY_EDITOR
                    Debug.LogError("WindowsNavigator not found in the scene.");
#endif
                    return;
                }
            }

            cameraProvider ??= new CameraProvider();

            windowsNavigator.Initialize();
            windowsNavigator.Show<LoadingWindow>();
            await saveLoadHandler.LoadAsync();
            resourcesProvider.Initialize();
            audioPlayer.Initialization();
            audioPlayer.PlayMusic("core-ambient");


            Application.targetFrameRate = 60;
            Application.runInBackground = true;
            Application.quitting += async () =>
            {
                await saveLoadHandler.SaveAsync();
#if UNITY_EDITOR
                Debug.Log("Game is quitting, saving data...");
#endif
            };

            await Task.Delay(2000); // Симулируем задержку для загрузки данных, например если бы использовали Addressables или Resources

            SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive).completed += (operation) =>
            {
                windowsNavigator.Show<MainMenuWindow>();
#if UNITY_EDITOR
                Debug.Log("MainMenu scene loaded successfully.");
#endif
            };
        }

        public void ApplicationQuit() =>
            Application.Quit();
    }
}