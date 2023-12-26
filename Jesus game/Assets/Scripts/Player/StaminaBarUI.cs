using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBarUI : MonoBehaviour
{
    [SerializeField]
    private Image _staminaBarForegroundImage;

    [SerializeField]
    private float fillSpeed = 100f;

    private void Start()
    {
        StartCoroutine(FillStaminaBarOverTime());
    }

    private IEnumerator FillStaminaBarOverTime()
    {
        float maxStamina = 10.0f;

        while (true)
        {
            if (_staminaBarForegroundImage.fillAmount < 1.0f)
            {
                yield return new WaitForSeconds(1);
            }

            // Regenerate gradually
            _staminaBarForegroundImage.fillAmount += fillSpeed * Time.deltaTime / maxStamina;

            yield return null;
        }
    }
}
