using Assets.Project.Code.Runtime.Architecture.Engine;
using Assets.Project.Code.Runtime.Gameplay.Common.AudioSystem;
using Assets.Project.Code.Runtime.Gameplay.Common.Extencions;
using Assets.Project.Code.Runtime.Gameplay.Common.LevelSystem;
using Assets.Project.Code.Runtime.Progress;
using System;
using UnityEngine;

namespace Assets.Project.Code.Runtime.Gameplay.Common.InteriorSystem
{
    public class InteriorEntity : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField]
        private InteriorEntityData interiorEntityData;

        [Header("VFX")]
        [SerializeField]
        private ParticleSystem interiorParticleSystem;
        [SerializeField]
        private ParticleSystem upgradeParticleSystem;

        [Header("State")]
        [SerializeField]
        private int id = -1;
        [SerializeField]
        private bool isActive = false;
        [SerializeField]
        private int currentLevel = -1;
        [SerializeField]
        private int maxLevel => interiorEntityData.Upgrades.Length - 1;

        [Header("Components")]
        [SerializeField]
        private Transform visitorPosition;
        [SerializeField]
        private InteriorQueuePoint interiorQueue;

        [SerializeField]
        private ProgressBar progressBar;

        public event Action<float> OnStartProgress;

        public int ID
        {
            get => id;
            set => id = value;
        }
        public bool IsActive
            => isActive;
        public int CurrentLevel => currentLevel;

        public bool IsMaxedLevel => currentLevel >= maxLevel;

        public UpgradeData CurrentUpgradeData
           => interiorEntityData.Upgrades[currentLevel < 0 ? 0 : currentLevel];

        public UpgradeData NextUpgradeData =>
            ((currentLevel + 1) < maxLevel) ?
            interiorEntityData.Upgrades[currentLevel + 1] :
            interiorEntityData.Upgrades[maxLevel];


        public InteriorEntityData InteriorEntityData
            => interiorEntityData;

        public Vector3 VisitorPosition =>
            visitorPosition.position;

        public InteriorQueuePoint InteriorQueue =>
            interiorQueue;

        [field: SerializeField]
        public int ParentSectionId { get; set; }


        public void SetData(InteriorSaveData data)
        {
            isActive = data.IsActive;
            currentLevel = data.UpgradeLevel;
            gameObject.SetActive(data.IsActive);
        }

        public void Open()
        {
            gameObject.SetActive(true);
            isActive = true;
            currentLevel = 0;
        }

        public void Close()
        {
            isActive = false;
            gameObject.SetActive(false);
        }

        public void Upgrade()
        {
            if (currentLevel >= maxLevel) return;
            currentLevel++;
            upgradeParticleSystem.Play();
            EngineSystem.Instance.AudioPlayer.Play("levelup", MixerTarget.SFX);
        }

        /// <summary>
        /// Нужен рефакториг, что бы не использовать свитч для каждого типа интерьера.
        /// Сделано для быстроты выполнения ТЗ
        /// </summary>
        public void Procces()
        {
            interiorParticleSystem?.Play();

            switch (interiorEntityData.Type)
            {
                case InteriorType.Freezer:
                    EngineSystem.Instance.AudioPlayer.Play("cash_register", MixerTarget.SFX);
                    LevelManager.Instance.CurrencyProvider.AddMoney(interiorEntityData.Upgrades[currentLevel].Value);
                    break;
                case InteriorType.CashRegister:
                    //OnStartProgress?.Invoke(CurrentUpgradeData.Value);
                    progressBar.FillTo(CurrentUpgradeData.Value);
                    EngineSystem.Instance.AudioPlayer.Play("cash_register", MixerTarget.SFX);
                    break;
                default:
                    break;
            }
        }
    }
}