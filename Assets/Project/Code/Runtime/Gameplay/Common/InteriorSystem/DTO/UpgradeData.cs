using System;
using UnityEngine;

namespace Assets.Project.Code.Runtime.Gameplay.Common.InteriorSystem
{
    [Serializable]
    public sealed class UpgradeData
    {
        [field: SerializeField]
        public AttributeType AttributeType { get; set; }
        [field: SerializeField]
        public float Value { get; set; }
        [field: SerializeField]
        public float Price { get; set; }
    }
}