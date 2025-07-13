using UnityEngine;

namespace Assets.Project.Code.Runtime.Gameplay.Common.InteriorSystem
{
    [CreateAssetMenu(fileName = "InteriorEntityData", menuName = "Game / LevelManager / Interior Entity Data", order = 1)]
    public class InteriorEntityData : ScriptableObject
    {
        [field: SerializeField]
        public int Id { get; set; }
        [field: SerializeField]
        public InteriorType Type { get; set; }
        [field: SerializeField]
        public string Name { get; set; }
        [field: SerializeField]
        public string Description { get; set; }
        [field: SerializeField]
        public int AmounVisitors { get; set; }
        [field: SerializeField]
        public InteriorEntity InteriorEntity { get; set; }
        [field: SerializeField]
        public UpgradeData[] Upgrades { get; set; }
    }
}