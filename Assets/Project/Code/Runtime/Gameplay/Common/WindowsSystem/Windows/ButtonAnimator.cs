using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Project.Code.Runtime.Gameplay.Common.WindowsSystem.Windows
{
    public class ButtonAnimator : MonoBehaviour
    {
        [SerializeField]
        private RectTransform[] buttons;
        [SerializeField]
        private float animationDuration = 1f;
        [SerializeField]
        private float delayBetweenButtons = 0.5f;
        [SerializeField]
        private float targetYPosition = 0f;

        [SerializeField]
        private VerticalLayoutGroup layoutGroup;

        [SerializeField]
        private AnimationCurve easeCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

        [SerializeField]
        private Vector2[] initialPositions;

        private void Awake()
        {
            initialPositions = new Vector2[buttons.Length];
            for (int i = 0; i < buttons.Length; i++)
            {
                initialPositions[i] = buttons[i].anchoredPosition;
                buttons[i].anchoredPosition = initialPositions[i] + Vector2.right * 200f;
                buttons[i].gameObject.SetActive(false);
            }
        }

        public void StartAnimation() =>
            StartCoroutine(AnimateButtons());

        private IEnumerator AnimateButtons()
        {
            yield return null;

            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].gameObject.SetActive(true);
                StartCoroutine(AnimateButton(buttons[i], initialPositions[i]));
                yield return new WaitForSeconds(delayBetweenButtons);
            }
        }

        private IEnumerator AnimateButton(RectTransform button, Vector2 targetPosition)
        {
            Vector2 startPosition = button.anchoredPosition;
            float elapsedTime = 0f;

            while (elapsedTime < animationDuration)
            {
                elapsedTime += Time.deltaTime;
                float t = Mathf.Clamp01(elapsedTime / animationDuration);
                button.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, easeCurve.Evaluate(t));
                yield return null;
            }
            button.anchoredPosition = targetPosition;
        }
    }
}