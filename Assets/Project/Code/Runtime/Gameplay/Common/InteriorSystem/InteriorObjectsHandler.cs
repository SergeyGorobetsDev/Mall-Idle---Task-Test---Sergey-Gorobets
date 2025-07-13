using Assets.Project.Code.Runtime.Architecture.Engine;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Assets.Project.Code.Runtime.Gameplay.Common.InteriorSystem
{

    [CanEditMultipleObjects]
    public sealed class InteriorObjectsHandler : MonoBehaviour, IInteriorObjectsHandler
    {
        public readonly Dictionary<InteriorType, List<InteriorEntity>> InteriorObjects = new Dictionary<InteriorType, List<InteriorEntity>>(2)
        {
            { InteriorType.CashRegister, new List<InteriorEntity>() },
            { InteriorType.Freezer, new List<InteriorEntity>() }
        };

        public void AddInteriorObject(InteriorType type, InteriorEntity entity)
        {
            if (!InteriorObjects.ContainsKey(type))
            {
                InteriorObjects[type] = new List<InteriorEntity>();
            }
            InteriorObjects[type].Add(entity);
            EngineSystem.Instance.SaveLoadHandler.ProgressData.SectionsSaveData
                .Find(data => data.ID == entity.ParentSectionId)
                .InteriorsSaveData.Find(item => item.ID == entity.ID).IsActive = true;
#if UNITY_EDITOR
            Debug.Log($"Added {entity.name} to {type} interior objects.");
#endif
        }

        public void RemoveInteriorObject(InteriorType type, InteriorEntity entity)
        {
            if (InteriorObjects.ContainsKey(type) && InteriorObjects[type].Contains(entity))
            {
                InteriorObjects[type].Remove(entity);
#if UNITY_EDITOR
                Debug.Log($"Removed {entity.name} from {type} interior objects.");
#endif
            }
            else
            {
#if UNITY_EDITOR
                Debug.LogWarning($"Attempted to remove {entity.name} from {type} interior objects, but it was not found.");
#endif
            }
        }

        public List<InteriorEntity> GetInteriorObjects(InteriorType type)
        {
            if (InteriorObjects.TryGetValue(type, out var entities))
                return entities;
#if UNITY_EDITOR
            Debug.LogWarning($"No interior objects found for type {type}.");
#endif
            return new List<InteriorEntity>();
        }

        public void ClearInteriorObjects()
        {
            InteriorObjects.Clear();
#if UNITY_EDITOR
            Debug.Log("All interior objects have been cleared.");
#endif
        }
    }
}
