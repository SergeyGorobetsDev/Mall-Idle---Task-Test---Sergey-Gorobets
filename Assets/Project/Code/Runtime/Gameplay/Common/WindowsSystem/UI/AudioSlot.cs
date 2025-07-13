using Assets.Project.Code.Runtime.Architecture.Engine;
using Assets.Project.Code.Runtime.Gameplay.Common.AudioSystem;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Project.Code.Runtime.Gameplay.Common.WindowsSystem.UI
{
    public sealed class AudioSlot : MonoBehaviour, IPointerEnterHandler
    {
        public void OnPointerEnter(PointerEventData eventData) =>
            EngineSystem.Instance.AudioPlayer.Play("hover", MixerTarget.UI);
    }
}