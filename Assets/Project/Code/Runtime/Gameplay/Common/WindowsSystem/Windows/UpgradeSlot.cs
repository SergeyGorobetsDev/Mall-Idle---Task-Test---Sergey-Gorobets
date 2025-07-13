using Assets.Project.Code.Runtime.Gameplay.Common.InteriorSystem;
using Assets.Project.Code.Runtime.Gameplay.Common.LevelSystem;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Project.Code.Runtime.Gameplay.Common.WindowsSystem.Windows
{
    public class UpgradeSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("UI Elements")]
        [SerializeField]
        private int slotIndex;
        [SerializeField]
        private bool isOpened = false;
        [SerializeField]
        private Image closeHoverImage;
        [SerializeField]
        private Image lockHoverImage;
        [SerializeField]
        private Button upgradeButton;
        [SerializeField]
        private TMP_Text upgradeText;
        [SerializeField]
        private Outline outline;

        private InteriorEntity interiorEntity;

        public event Action<int> OnUpgradeSlotClicked;

        private void OnEnable() =>
            upgradeButton.onClick.AddListener(() =>
                OnUpgradeSlotClicked?.Invoke(slotIndex));

        private void OnDisable() =>
            upgradeButton.onClick.RemoveAllListeners();

        public void SetData(int id) =>
            this.slotIndex = id;

        public void SetData(InteriorEntity interiorEntity)
        {
            this.interiorEntity = interiorEntity;
        }

        public void ChangeStateData()
        {
            if (LevelManager.Instance.LevelSectionsManager.IsSectionOpened(interiorEntity.ParentSectionId))
            {
                closeHoverImage.gameObject.SetActive(false);
                lockHoverImage.gameObject.SetActive(false);

                upgradeButton.interactable = LevelManager.Instance.CurrencyProvider.GetMoney() >= interiorEntity.NextUpgradeData.Price;
                upgradeText.text = interiorEntity.NextUpgradeData.Price.ToString();

                if (interiorEntity.IsMaxedLevel)
                {
                    upgradeButton.interactable = false;
                    upgradeText.text = "Upgraded";
                }
            }
            else
            {
                closeHoverImage.gameObject.SetActive(true);
                lockHoverImage.gameObject.SetActive(true);
                upgradeButton.interactable = false;
                upgradeText.text = "Locked";
            }
        }

        public void OnPointerEnter(PointerEventData eventData) =>
          outline.enabled = true;
        public void OnPointerExit(PointerEventData eventData) =>
            outline.enabled = false;
    }
}