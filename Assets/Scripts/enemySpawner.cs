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
    public GameObject instant;
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
        instant = Instantiate(enemy, navigate.position, enemy.transform.rotation);
        StartCoroutine(MoveEnemy(instant));
    }

    IEnumerator MoveEnemy(GameObject instant)
    {
             difference = new Vector3(
              instant.transform.position.x - hedef.position.x,
              instant.transform.position.y - hedef.position.y,
              instant.transform.position.z - hedef.position.z);
        // D��man hedefe do�ru hareket eder
               distance = Math.Sqrt(
              Math.Pow(difference.x, 2f) +
              Math.Pow(difference.y, 2f) +
              Math.Pow(difference.z, 2f));

        while (instant != null && distance > konum)
        {
                instant.transform.position = Vector3.MoveTowards(
                    instant.transform.position,
                    hedef.position,
                    speed * Time.deltaTime
                );
            difference = new Vector3(
              instant.transform.position.x - hedef.position.x,
              instant.transform.position.y - hedef.position.y,
              instant.transform.position.z - hedef.position.z);
            distance = Math.Sqrt(
            Math.Pow(difference.x, 2f) +
            Math.Pow(difference.y, 2f) +
            Math.Pow(difference.z, 2f));
            yield return null; // Bir sonraki kareye kadar bekle
        }
        // Mesafe 0.5f'ye e�it veya daha k���kse
        if (instant != null && Vector3.Distance(instant.transform.position, hedef.position) <= konum)
        {
            movementEnemy();
            yield return new WaitForSeconds(1f); // 1 saniye bekle
            Destroy(instant); // D��man� yok et
        }
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.5f);
    }
    void movementEnemy()
    {
        // 5 kez top f�rlatma i�lemi
        if (counter < 5)
        {
            Debug.Log("top f�rlatt�");
            ballSpawner.Instance.ballInstant();
            counter++;
        }
    }
}
