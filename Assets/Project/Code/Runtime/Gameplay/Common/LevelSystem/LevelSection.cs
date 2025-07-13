using Assets.Project.Code.Runtime.Gameplay.Common.InteriorSystem;
using Assets.Project.Code.Runtime.Progress;
using UnityEngine;

namespace Assets.Project.Code.Runtime.Gameplay.Common.LevelSystem
{
    public sealed class LevelSection : MonoBehaviour
    {
        [SerializeField]
        private int sectionId;
        [SerializeField]
        private bool isOpened = false;

        [SerializeField]
        private GameObject sectionContent;
        [SerializeField]
        private InteriorEntity[] interiorEntities;

        public int SectionId => sectionId;
        public bool IsOpened => isOpened;

        public InteriorEntity[] InteriorEntities => interiorEntities;

        public void SetData(SectionSaveData sectionSaveData)
        {
            isOpened = sectionSaveData.IsOpened;
            sectionContent.SetActive(sectionSaveData.IsOpened);
            for (int i = 0; i < interiorEntities.Length; i++)
            {
                interiorEntities[i].SetData(sectionSaveData.InteriorsSaveData.Find(data => data.ID == interiorEntities[i].ID));
                if (interiorEntities[i].IsActive)
                    LevelManager.Instance.InteriorObjectsHandler.AddInteriorObject(interiorEntities[i].InteriorEntityData.Type, interiorEntities[i]);
            }
        }

        public void Open()
        {
            sectionContent.SetActive(true);
            isOpened = true;
        }

        public void Close()
        {
            sectionContent.SetActive(false);
            isOpened = false;
            CloseAllInterior();
        }

        private void CloseAllInterior()
        {
            foreach (var entity in interiorEntities)
                if (entity != null) entity.Close();
        }

        public void InitializeInteriorItems()
        {
            for (int i = 0; i < interiorEntities.Length; i++)
            {
                interiorEntities[i].ID = i;
                interiorEntities[i].ParentSectionId = sectionId;
            }
        }
    }
}