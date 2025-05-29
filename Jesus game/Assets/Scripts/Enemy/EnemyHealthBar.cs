using UnityEngine;
using TMPro;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField]
    private UnityEngine.UI.Image _healthBarForegroundImage;
    [SerializeField]
    private TMP_Text _healthChangeText;

    public void UpdateHealthBar(EnemyHealthController EnemyhealthController)
    {
        _healthBarForegroundImage.fillAmount = EnemyhealthController.RemainingHealthPercentage;
    }

    public void ShowHealthChange(float changeAmount, bool healthAdded)
    {
        _healthChangeText.text = changeAmount.ToString();

        if (!healthAdded)
        {
            _healthChangeText.color = Color.red;
            _healthChangeText.text = "-" + changeAmount.ToString();
        }
        else
        {
            _healthChangeText.color = Color.green;
            _healthChangeText.text = changeAmount.ToString();
        }
    }
}
