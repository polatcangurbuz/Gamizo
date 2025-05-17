using System;
using UnityEngine;

public class characterHealth : MonoBehaviour
{
   
    public static characterHealth Instance { get; private set; }

    public event Action<int> OnHealthChanged; // Health de�i�ti�inde �a�r�lacak event


    public int health = 100;
    public int Health
    {
        get => health;
        set
        {
            health = Mathf.Clamp(value, 0, 100); // 0-100 aras�nda s�n�rla
            OnHealthChanged?.Invoke(health); // De�i�iklik oldu�unda event'i tetikle
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
