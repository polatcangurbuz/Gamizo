using System;
using System.Collections;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{
    [SerializeField] Transform navigate; // Spawn pozisyonu
    [SerializeField] GameObject enemy;  // D��man prefab�
    [SerializeField] Transform hedef;  // Hedef nesne
    [SerializeField] float speed = 0.5f; // H�z ve mesafe kontrol�
    [SerializeField] float spawnInterval = 5f; // Spawn aral���
    public GameObject instantEnemy;
    int counter = 0; // Ate� etme sayac�
    float timer = 0,tempmesafe; // Spawn zamanlay�c�
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

        // Spawn aral��� dolduysa yeni d��man olu�tur
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
        // D��man hedefe do�ru hareket eder
        while (instant != null)
        {
            // D��man ve hedef aras�ndaki mesafeyi hesapla
            difference = instant.transform.position - hedef.position;
            distance = Math.Sqrt(
                Math.Pow(difference.x, 2f) +
                Math.Pow(difference.y, 2f) +
                Math.Pow(difference.z, 2f));

            // E�er mesafe konum de�erinden k���kse, ate� et ve d��man� yok et
            if (distance <= konum)
            {
                if (instant != null) // G�venlik kontrol�
                {
                    ballSpawner.Instance.FireBall(instant, hedef);
                    yield return new WaitForSeconds(1f);

                    // Nesne h�l� mevcut mu kontrol et
                    if (instant != null)
                    {
                        Destroy(instant);
                    }
                }
                yield break; // Coroutine'i tamamen sonland�r
            }

            // D��man� hedefe do�ru hareket ettir
            if (instant != null) // G�venlik kontrol�
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
