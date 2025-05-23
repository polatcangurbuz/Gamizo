using System;
using UnityEngine;

public class characterHealth : MonoBehaviour
{
    public static characterHealth Instance { get; private set; }

    public event Action<int> OnHealthChanged; // Health değiştiğinde çağrılacak event

    private int health = 100;
    public int Health
    {
        get => health;
        set
        {
            health = Mathf.Clamp(value, 0, 100); // 0-100 arasında sınırla
            OnHealthChanged?.Invoke(health); // Değişiklik olduğunda event'i tetikle
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
