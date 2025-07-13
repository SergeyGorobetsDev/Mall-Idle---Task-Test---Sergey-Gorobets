using Assets.Project.Code.Runtime.Architecture.Engine;
using Assets.Project.Code.Runtime.Gameplay.Common.AudioSystem;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Project.Code.Runtime.Gameplay.Common.WindowsSystem.UI
{
    public sealed class AudioButton : MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            EngineSystem.Instance.AudioPlayer.Play("click", MixerTarget.UI);
        }
    }
}