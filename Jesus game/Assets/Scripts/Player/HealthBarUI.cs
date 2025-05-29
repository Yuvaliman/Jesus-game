using System.Collections;
using TMPro;
using UnityEngine;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField]
    private UnityEngine.UI.Image _healthBarForegroundImage;
    [SerializeField]
    private Canvas healthChangeCanvas;
    [SerializeField]
    private GameObject _healthChangeTextPrefab;
    [SerializeField] private float animationDuration = 1f;
    [SerializeField] private Vector3 moveOffset = new Vector3(0, 50f, 0);
    [SerializeField] private AnimationCurve fadeCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    private Coroutine currentRoutine;

    public void UpdateHealthBar(HealthController healthController)
    {
        _healthBarForegroundImage.fillAmount = healthController.RemainingHealthPercentage;
    }

    public void ShowHealthChange(float changeAmount, bool healthAdded)
    {
        // Reset text and color  
        string prefix = healthAdded ? "" : "-";
        GameObject healthChangeTextObject = Instantiate(_healthChangeTextPrefab, healthChangeCanvas.transform, false);
        TMP_Text _healthChangeText = healthChangeTextObject.GetComponent<TMP_Text>();
        _healthChangeText.text = prefix + Mathf.Abs(changeAmount).ToString();
        _healthChangeText.color = healthAdded ? Color.green : Color.red;
        _healthChangeText.canvasRenderer.SetAlpha(0f);

        // Restart animation  
        if (currentRoutine != null)
            StopCoroutine(currentRoutine);

        currentRoutine = StartCoroutine(AnimateHealthText(_healthChangeText)); // Pass the text object to the coroutine  
    }

    private IEnumerator AnimateHealthText(TMP_Text _healthChangeText) // Accept the text object as a parameter
    {
        Vector3 startPos = _healthChangeText.rectTransform.localPosition;
        Vector3 endPos = startPos + moveOffset;
        float timer = 0f;

        while (timer < animationDuration)
        {
            float t = timer / animationDuration;

            // Fade in then out using curve
            float alpha = 1f - Mathf.Abs((t * 2f) - 1f); // Peak at t=0.5
            alpha = fadeCurve.Evaluate(alpha);

            _healthChangeText.canvasRenderer.SetAlpha(alpha);
            _healthChangeText.rectTransform.localPosition = Vector3.Lerp(startPos, endPos, t);

            timer += Time.deltaTime;
            yield return null;
        }

        Destroy(_healthChangeText.gameObject); // Clean up the text object after animation
    }
}
