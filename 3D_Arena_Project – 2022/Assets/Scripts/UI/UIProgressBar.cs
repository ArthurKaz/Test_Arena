using System.Collections;
using Test_Project.Abstractions;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIProgressBar : MonoBehaviour
    {
        [SerializeField] private Image progress;
        private IProgressBarValue _value;

        public void Init(IProgressBarValue value)
        {
            _value = value;
            _value.ProgressChanged += ShowChanges;
        }

        private void ShowChanges()
        {
            StartCoroutine(SmoothlyChangeSliderValue(_value.Progress));
        }

        private IEnumerator SmoothlyChangeSliderValue(float targetValue)
        {
            float currentValue = progress.fillAmount;
            float t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime / 0.5f;
                progress.fillAmount = Mathf.Lerp(currentValue, targetValue, t);
                yield return null;
            }
        }
    }
}