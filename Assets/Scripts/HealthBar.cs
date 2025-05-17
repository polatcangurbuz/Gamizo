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
            UpdateHealthBar(characterHealth.Instance.Health); // Baþlangýçta da güncelle
        }
    }

    private void OnDestroy()
    {
        if (characterHealth.Instance != null)
        {
            characterHealth.Instance.OnHealthChanged -= UpdateHealthBar; // Event'ten çýk
        }
    }

    private void UpdateHealthBar(int currentHealth)
    {       
        healthImage.transform.localScale = new Vector3((currentHealth / 100f), 1, 1);
        Debug.Log("Health Bar Güncellendi: " + healthImage.fillAmount);
    }
}
