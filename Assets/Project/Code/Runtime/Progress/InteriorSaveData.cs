using Newtonsoft.Json;
using System;
using UnityEngine;

namespace Assets.Project.Code.Runtime.Progress
{
    [Serializable]
    public sealed class InteriorSaveData
    {
        [field: SerializeField, JsonProperty]
        public int ID { get; set; }

        [field: SerializeField, JsonProperty]
        public bool IsActive { get; set; }

        [field: SerializeField, JsonProperty]
        public int UpgradeLevel { get; set; }
    }
}