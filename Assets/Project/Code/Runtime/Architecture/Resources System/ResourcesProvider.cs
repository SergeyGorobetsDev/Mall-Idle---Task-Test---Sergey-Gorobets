using UnityEngine;

namespace Assets.Project.Code.Runtime.Architecture.Resources_System
{
    public interface IResourcesProvider
    {
    }

    public sealed class ResourcesProvider : MonoBehaviour, IResourcesProvider
    {
        public void Initialize()
        {
        }
    }
}
