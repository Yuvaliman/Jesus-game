using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField]
    private UnityEngine.UI.Image _healthBarForegroundImage;

    public void UpdateHealthBar(EnemyHealthController EnemyhealthController)
    {
        _healthBarForegroundImage.fillAmount = EnemyhealthController.RemainingHealthPercentage;
    }
}
