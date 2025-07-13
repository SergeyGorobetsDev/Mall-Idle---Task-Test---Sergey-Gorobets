using Assets.Project.Code.Runtime.Gameplay.Common.InteriorSystem;
using System.Collections.Generic;

namespace Assets.Project.Code.Runtime.Gameplay.Common.LevelSystem
{
    public interface ILevelSectionsManager
    {
        void CloseAllSections();
        List<InteriorEntity> GetAllInteriorEntities();
        InteriorEntity[] GetInteriorEntities(int sectionId);
        void Initialize();
        bool IsSectionOpened(int sectionId);
        void OpenSection(int sectionId);
    }
}