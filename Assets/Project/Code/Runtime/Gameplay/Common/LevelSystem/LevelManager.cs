using Assets.Project.Code.Runtime.Architecture.Engine;
using Assets.Project.Code.Runtime.Gameplay.Common.Currency;
using Assets.Project.Code.Runtime.Gameplay.Common.InteriorSystem;
using Assets.Project.Code.Runtime.Gameplay.Common.SpwnerSystem;
using Assets.Project.Code.Runtime.Gameplay.Common.WindowsSystem.Windows;
using UnityEngine;

namespace Assets.Project.Code.Runtime.Gameplay.Common.LevelSystem
{
    /// <summary>
    /// Входная точка для управления игровым уровнем.
    /// Используется для инициализации скриптов сцены перед началом игрового процесса.
    /// Хранит в себе ссылки на все игровые системы, которые будут использоваться в процессе игры.
    /// </summary>
    public sealed class LevelManager : MonoBehaviour
    {
        private const int defaultOpenSectionId = 0;

        [Header("Gameplay Systems")]
        [SerializeField]
        private CurrancyProvider currencyProvider = new();

        [Space()]
        [Header("Visitors Manager")]
        [SerializeField]
        private InteriorObjectsHandler interiorObjectsHandler;

        [Space()]
        [Header("Visitors Manager")]
        [SerializeField]
        private LevelSectionsManager levelSectionsManager;

        [Space()]
        [Header("Visitors Manager")]
        [SerializeField]
        private VisitorsProvider visitorsProvider;

        [Space()]
        [Header("Actors Spawner")]
        [SerializeField]
        private ActorsSpawnHandler actorsSpawnHandler;

        [SerializeField]
        private LevelSection[] mallSections;

        public ICurrancyProvider CurrencyProvider => currencyProvider;
        public IInteriorObjectsHandler InteriorObjectsHandler => interiorObjectsHandler;
        public ILevelSectionsManager LevelSectionsManager => levelSectionsManager;
        public IVisitorsProvider VisitorsProvider => visitorsProvider;
        public IActorsSpawnHandler ActorsSpawnHandler => actorsSpawnHandler;

        public static LevelManager Instance { get; private set; }

        private void Awake() =>
            Instance = this;

        private void Start() =>
            Initialize();

        private void Update()
        {
            visitorsProvider.Tick();
        }

        public void Initialize()
        {
            EngineSystem.Instance.CameraProvider.SetMainCamera(Camera.main);
            levelSectionsManager = new(mallSections);
            levelSectionsManager.Initialize();
            currencyProvider.Initialize(EngineSystem.Instance.SaveLoadHandler.ProgressData.Money);
            visitorsProvider.Initialize(EngineSystem.Instance.SaveLoadHandler.ProgressData.AvailableVisitorAmoung);
            EngineSystem.Instance.WindowsNavigator.Show<GameplayWindow>();
        }
    }
}