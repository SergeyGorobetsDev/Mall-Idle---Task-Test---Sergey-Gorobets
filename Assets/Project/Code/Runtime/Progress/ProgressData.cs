using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Project.Code.Runtime.Progress
{
    [Serializable]
    public sealed class ProgressData
    {
        [field: SerializeField, JsonProperty]
        public string Version { get; set; }

        [field: SerializeField, JsonProperty]
        public bool NewGame { get; set; }

        [field: SerializeField, JsonProperty]
        public int Level { get; set; }
        [field: SerializeField, JsonProperty]
        public float Expirience { get; set; }
        [field: SerializeField, JsonProperty]
        public float Money { get; set; }
        [field: SerializeField]
        public int AvailableVisitorAmoung { get; set; }

        [field: SerializeField, JsonProperty]
        public List<int> LevelSectionsIds { get; set; }

        [field: SerializeField, JsonProperty]
        public List<InteriorOpenData> InteriorsOpenData { get; set; }

        [field: SerializeField, JsonProperty]
        public List<SectionSaveData> SectionsSaveData { get; set; }
    }
}