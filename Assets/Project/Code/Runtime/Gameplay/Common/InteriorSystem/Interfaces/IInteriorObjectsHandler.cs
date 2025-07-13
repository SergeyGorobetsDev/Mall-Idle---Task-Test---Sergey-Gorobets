using System.Collections.Generic;

namespace Assets.Project.Code.Runtime.Gameplay.Common.InteriorSystem
{
    public interface IInteriorObjectsHandler
    {
        void AddInteriorObject(InteriorType type, InteriorEntity entity);
        void RemoveInteriorObject(InteriorType type, InteriorEntity entity);
        List<InteriorEntity> GetInteriorObjects(InteriorType type);
        void ClearInteriorObjects();
    }
}