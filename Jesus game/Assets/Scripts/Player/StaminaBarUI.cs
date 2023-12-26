using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBarUI : MonoBehaviour
{
    [SerializeField]
    private Image _staminaBarForegroundImage;

    private void Start()
    {
        StartCoroutine(FillStaminaBarOverTime());
    }

    private IEnumerator FillStaminaBarOverTime()
    {
        while (true)
        {
            if (_staminaBarForegroundImage.fillAmount < 1.0f)
            {
                yield return new WaitForSeconds(1);
            }

            // Regenerate gradually
            _staminaBarForegroundImage.fillAmount += 10f * Time.deltaTime;

            yield return null;
        }
    }
}
