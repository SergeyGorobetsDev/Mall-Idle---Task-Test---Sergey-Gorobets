using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Project.Code.Runtime.Gameplay.Common.Extencions
{
    public sealed class Tab : MonoBehaviour, IPointerClickHandler
    {
        [Header("Tab Data")]
        [SerializeField]
        private int tabIndex;

        [SerializeField]
        private Image tabImage;
        [SerializeField]
        private TMP_Text tabText;

        [Header("Tab Colors")]
        [SerializeField]
        private Color normalColor;
        [SerializeField]
        private Color HighlightColor;

        public event Action<int> OnTabSelected;

        public void SetData(int index) =>
            tabIndex = index;

        public void OnPointerClick(PointerEventData eventData) =>
            OnTabSelected?.Invoke(tabIndex);

        public void ChangeState(bool isSelected)
        {
            tabImage.color = isSelected ? HighlightColor : normalColor;
            tabText.color = isSelected ? normalColor : HighlightColor;
        }
    }
}