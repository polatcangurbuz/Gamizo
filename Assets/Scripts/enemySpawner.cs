using System;
using System.Collections;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{
    [SerializeField] Transform navigate; // Spawn pozisyonu
    [SerializeField] GameObject enemy;  // Düþman prefabý
    [SerializeField] Transform hedef;  // Hedef nesne
    [SerializeField] float speed = 0.5f; // Hýz ve mesafe kontrolü
    [SerializeField] float spawnInterval = 5f; // Spawn aralýðý
    public GameObject instantEnemy;
    int counter = 0; // Ateþ etme sayacý
    float timer = 0,tempmesafe; // Spawn zamanlayýcý
    Vector3 difference;
    double distance, konum = 0.3f;
    public static enemySpawner Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;

        // Spawn aralýðý dolduysa yeni düþman oluþtur
        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    void SpawnEnemy()
    {
        instantEnemy = Instantiate(enemy, navigate.position, enemy.transform.rotation);
        StartCoroutine(MoveEnemy(instantEnemy));
    }

    IEnumerator MoveEnemy(GameObject instant)
    {
        // Düþman hedefe doðru hareket eder
        while (instant != null)
        {
            // Düþman ve hedef arasýndaki mesafeyi hesapla
            difference = instant.transform.position - hedef.position;
            distance = Math.Sqrt(
                Math.Pow(difference.x, 2f) +
                Math.Pow(difference.y, 2f) +
                Math.Pow(difference.z, 2f));

            // Eðer mesafe konum deðerinden küçükse, ateþ et ve düþmaný yok et
            if (distance <= konum)
            {
                if (instant != null) // Güvenlik kontrolü
                {
                    ballSpawner.Instance.FireBall(instant, hedef);
                    yield return new WaitForSeconds(1f);

                    // Nesne hâlâ mevcut mu kontrol et
                    if (instant != null)
                    {
                        Destroy(instant);
                    }
                }
                yield break; // Coroutine'i tamamen sonlandýr
            }

            // Düþmaný hedefe doðru hareket ettir
            if (instant != null) // Güvenlik kontrolü
            {
                instant.transform.position = Vector3.MoveTowards(
                    instant.transform.position,
                    hedef.position,
                    speed * Time.deltaTime
                );
            }

            yield return null; // Bir sonraki kareye kadar bekle
        }
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.5f);
    }
   
   
}
