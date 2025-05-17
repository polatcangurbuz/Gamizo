using System;
using UnityEngine;

public class characterHealth : MonoBehaviour
{
   
    public static characterHealth Instance { get; private set; }

    public event Action<int> OnHealthChanged; // Health deðiþtiðinde çaðrýlacak event


    public int health = 100;
    public int Health
    {
        get => health;
        set
        {
            health = Mathf.Clamp(value, 0, 100); // 0-100 arasýnda sýnýrla
            OnHealthChanged?.Invoke(health); // Deðiþiklik olduðunda event'i tetikle
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
