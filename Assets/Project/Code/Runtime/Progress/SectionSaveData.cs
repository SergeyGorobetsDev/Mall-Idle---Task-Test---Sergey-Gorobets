using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Project.Code.Runtime.Progress
{
    [Serializable]
    public sealed class SectionSaveData
    {
        [field: SerializeField, JsonProperty]
        public int ID { get; set; }

        [field: SerializeField, JsonProperty]
        public bool IsOpened { get; set; }

        [field: SerializeField, JsonProperty]
        public List<InteriorSaveData> InteriorsSaveData { get; set; }
    }
}