using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image healthImage;

    private void Start()
    {
        if (characterHealth.Instance != null)
        {
            characterHealth.Instance.OnHealthChanged += UpdateHealthBar; // Event'e abone ol
            UpdateHealthBar(characterHealth.Instance.Health); // Ba�lang��ta da g�ncelle
        }
    }

    private void OnDestroy()
    {
        if (characterHealth.Instance != null)
        {
            characterHealth.Instance.OnHealthChanged -= UpdateHealthBar; // Event'ten ��k
        }
    }

    private void UpdateHealthBar(int currentHealth)
    {
        healthImage.fillAmount = currentHealth / 100f;
        Debug.Log("Health Bar G�ncellendi: " + healthImage.fillAmount);
    }
}
