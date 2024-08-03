using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBarUI : MonoBehaviour
{
    [SerializeField]
    private Image _staminaBarForegroundImage;

    private float targetFillAmount = 1.0f;
    private float fillSpeed = 0.1f;

    private void Start()
    {
        StartCoroutine(FillStaminaBar());
    }

    private IEnumerator FillStaminaBar()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f); // Wait for 1 second

            while (_staminaBarForegroundImage.fillAmount < 0.99f)
            {
                // Regenerate stamina
                _staminaBarForegroundImage.fillAmount = Mathf.MoveTowards(_staminaBarForegroundImage.fillAmount, targetFillAmount, fillSpeed * Time.deltaTime);

                // Ensure the stamina bar doesn't go below 0
                if (_staminaBarForegroundImage.fillAmount < 0.01f)
                {
                    _staminaBarForegroundImage.fillAmount = 0f;
                    yield return new WaitForSeconds(1.0f);
                    _staminaBarForegroundImage.fillAmount = 0.011f;
                }


                yield return null;
            }

            // Reset stamina bar for the next iteration
            ResetStaminaBar();
        }
    }

    private void ResetStaminaBar()
    {
        // Reset stamina bar to initial state
        _staminaBarForegroundImage.fillAmount = 1.0f;
    }
}
