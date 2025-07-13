using UnityEngine;

namespace Assets.Project.Code.Runtime.Gameplay.Common.SpwnerSystem
{
    public sealed class ActorSpawnPoint : MonoBehaviour
    {
        [SerializeField]
        private Transform parentTransform;

        private void Awake() =>
            parentTransform ??= transform;

        public Vector3 GetPosition() =>
            parentTransform.position;
    }
}