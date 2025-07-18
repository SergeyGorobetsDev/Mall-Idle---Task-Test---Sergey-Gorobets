using Assets.Project.Code.Runtime.Architecture.Engine;
using Assets.Project.Code.Runtime.Gameplay.Common.Extencions;
using Assets.Project.Code.Runtime.Gameplay.Common.LevelSystem;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Project.Code.Runtime.Gameplay.Common.InteriorSystem.UI
{
    public sealed class InteriorListView : TabContent
    {
        [SerializeField]
        private InteriorType interiorType;

        [SerializeField]
        private List<InteriorEntity> found;

        public override void Initialize()
        {
            base.Initialize();

            List<InteriorEntity> entities = LevelManager.Instance.LevelSectionsManager.GetAllInteriorEntities();
            found = entities.FindAll(e => e.InteriorEntityData.Type == interiorType);

            if (found == null) return;

            for (int i = 0; i < found.Count; i++)
            {
                upgradeSlots[i].SetData(found[i]);
                upgradeSlots[i].ChangeStateData();
            }

            Debug.Log("Initialize InteriorListView");

        }

        protected override void Register()
        {
            base.Register();
            LevelManager.Instance.CurrencyProvider.OnMoneyChanged += CheckSlotsCanBePurchase;
        }

        protected override void UnRegister()
        {
            base.UnRegister();
            LevelManager.Instance.CurrencyProvider.OnMoneyChanged -= CheckSlotsCanBePurchase;
        }

        protected override void UpgradeSlotClicked(int id)
        {
            Debug.Log("UpgradeSlotClicked");

            if (id < 0 || id >= found.Count)
                return;

            InteriorEntity entity = found[id];

            if (entity.IsActive)
                entity.Upgrade();
            else
            {
                entity.Open();
                LevelManager.Instance.InteriorObjectsHandler.AddInteriorObject(interiorType, entity);
            }

            upgradeSlots[id].ChangeStateData();
            LevelManager.Instance.CurrencyProvider.RemoveMoney(entity.CurrentUpgradeData.Price);
            EngineSystem.Instance.SaveLoadHandler.ProgressData.SectionsSaveData
                .Find(data => data.ID == entity.ParentSectionId)
                .InteriorsSaveData.Find(item => item.ID == entity.ID).UpgradeLevel = entity.CurrentLevel;

            if (interiorType is InteriorType.Freezer or InteriorType.Shelf)
                LevelManager.Instance.VisitorsProvider.IncrementMaxVisitors(entity.InteriorEntityData.AmounVisitors);


            if (!CheckIfAnySectionFullIntemsbBought())
                LevelManager.Instance.LevelSectionsManager.OpenSection(1);
            CheckSlotsCanBePurchase();
        }

        private void CheckSlotsCanBePurchase(float value = 0)
        {
            for (int i = 0; i < found.Count; i++)
                if (i < found.Count)
                    upgradeSlots[i].ChangeStateData();
        }

        private bool CheckIfAnySectionFullIntemsbBought() =>
            LevelManager.Instance.LevelSectionsManager.GetInteriorEntities(0)
            .Where(interior => interior.ParentSectionId == 0)
            .Any(interior => interior.IsActive == false);
    }
}