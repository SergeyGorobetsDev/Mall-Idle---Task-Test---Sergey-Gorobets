using Assets.Project.Code.Runtime.Architecture.Engine;
using Assets.Project.Code.Runtime.Gameplay.Common.InteriorSystem;
using Assets.Project.Code.Runtime.Progress;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Project.Code.Runtime.Gameplay.Common.LevelSystem
{
    [Serializable]
    public sealed class LevelSectionsManager : ILevelSectionsManager
    {
        private const int DefaulSectionToOpen = 0;

        [SerializeField]
        private LevelSection[] sections;

        public event Action<int> SectionOpened;

        public LevelSectionsManager(LevelSection[] sections) =>
            this.sections = sections;

        public void Initialize()
        {
            CloseAllSections();
            for (int i = 0; i < sections.Length; i++)
                sections[i].InitializeInteriorItems();

            List<SectionSaveData> sectionsSaveData = EngineSystem.Instance.SaveLoadHandler.ProgressData.SectionsSaveData;

            if (sectionsSaveData == null)
            {
                sectionsSaveData = new();

                for (int i = 0; i < sections.Length; i++)
                {
                    SectionSaveData sectionData = new();
                    sectionData.ID = sections[i].SectionId;
                    sectionData.IsOpened = sections[i].IsOpened;

                    List<InteriorSaveData> intetiors = new();
                    for (int j = 0; j < sections[i].InteriorEntities.Length; j++)
                    {
                        InteriorEntity interiorEntity = sections[i].InteriorEntities[j];

                        intetiors.Add(new InteriorSaveData
                        {
                            ID = interiorEntity.ID,
                            IsActive = interiorEntity.IsActive,
                            UpgradeLevel = interiorEntity.CurrentLevel
                        });
                    }

                    sectionData.InteriorsSaveData = intetiors;
                    sectionsSaveData.Add(sectionData);
                }

                EngineSystem.Instance.SaveLoadHandler.ProgressData.SectionsSaveData = sectionsSaveData;
                OpenSection(DefaulSectionToOpen);
                return;
            }

            for (int i = 0; i < sections.Length; i++)
                sections[i].SetData(sectionsSaveData[i]);
        }

        public InteriorEntity[] GetInteriorEntities(int sectionId)
        {
            if (sectionId < 0 || sectionId >= sections.Length)
            {
                Debug.LogError($"Invalid section ID: {sectionId}");
                return Array.Empty<InteriorEntity>();
            }
            return sections[sectionId].InteriorEntities;
        }

        public List<InteriorEntity> GetAllInteriorEntities()
        {
            List<InteriorEntity> allEntities = new(sections.Length);
            for (int i = 0; i < sections.Length; i++)
                allEntities.AddRange(sections[i].InteriorEntities);
            return allEntities;
        }

        public void OpenSection(int sectionId)
        {
            if (sectionId < 0 || sectionId >= sections.Length)
            {
                Debug.LogError($"Invalid section ID: {sectionId}");
                return;
            }

            if (sections[sectionId].IsOpened) return;

            sections[sectionId].Open();

            EngineSystem.Instance.SaveLoadHandler.ProgressData.SectionsSaveData
                .Find(section => section.ID == sectionId).IsOpened = true; ;
        }

        public void CloseAllSections()
        {
            foreach (var section in sections)
                section.Close();
        }

        public bool IsSectionOpened(int sectionId)
        {
            if (sectionId < 0 || sectionId >= sections.Length)
            {
                Debug.LogError($"Invalid section ID: {sectionId}");
                return false;
            }
            return sections[sectionId].IsOpened;
        }
    }
}
