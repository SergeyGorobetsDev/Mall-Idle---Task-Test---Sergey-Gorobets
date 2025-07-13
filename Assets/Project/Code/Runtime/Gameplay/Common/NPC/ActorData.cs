using UnityEngine;

namespace Assets.Project.Code.Runtime.Gameplay.Common.NPC
{
    [CreateAssetMenu(fileName = "ActorEntityData", menuName = "Game / Actors / ActorEntity Entity Data", order = 1)]
    public class ActorData : ScriptableObject
    {
        [field: SerializeField]
        public int Id { get; set; }
        [field: SerializeField]
        public ActorType ActorType { get; set; }
        [field: SerializeField]
        public float MovementSpeed { get; set; }
        [field: SerializeField]
        public float RotationSpeed { get; set; }
        [field: SerializeField]
        public ActorEntity ActorPrefab { get; set; }
    }
}