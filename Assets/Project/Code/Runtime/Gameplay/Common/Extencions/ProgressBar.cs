using Assets.Project.Code.Runtime.Gameplay.Common.InteriorSystem;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
namespace Assets.Project.Code.Runtime.Gameplay.Common.Extencions
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField]
        private Slider slider;
        [SerializeField]
        private InteriorEntity interiorEntity;

        private float fillSpeed = 1.0f;
        private float currentValue = 0f;
        private Coroutine fillCoroutine;

        private void Awake()
        {
            if (slider == null)
                slider = GetComponent<Slider>();

            slider.minValue = 0f;
            slider.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            if (interiorEntity != null)
                interiorEntity.OnStartProgress += FillTo;
        }

        private void OnDisable()
        {
            if (interiorEntity != null)
                interiorEntity.OnStartProgress -= FillTo;
        }

        public void FillTo(float targetValue)
        {
            Debug.Log($"Fill progress {targetValue}");

            slider.gameObject.SetActive(true);

            if (fillCoroutine != null)
                StopCoroutine(fillCoroutine);

            fillCoroutine = StartCoroutine(FillProgressCoroutine(targetValue));
        }

        private IEnumerator FillProgressCoroutine(float targetValue)
        {
            slider.minValue = 0;
            slider.maxValue = targetValue;

            while (currentValue < targetValue)
            {
                currentValue += fillSpeed * Time.deltaTime;
                //currentValue = Mathf.Min(currentValue, targetValue);
                slider.value = currentValue;
                yield return null;
            }

            fillCoroutine = null;
            slider.gameObject.SetActive(false);
        }
    }

}
