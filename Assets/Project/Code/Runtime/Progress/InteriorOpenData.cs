using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Project.Code.Runtime.Progress
{
    [Serializable]
    public sealed class InteriorOpenData
    {
        [field: SerializeField, JsonProperty]
        public int ParentSectionId { get; set; }

        [field: SerializeField, JsonProperty]
        public List<int> InteriorEntitiesIds { get; set; }
    }
}