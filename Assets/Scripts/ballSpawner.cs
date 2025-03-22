using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballSpawner : MonoBehaviour
{
    public GameObject ballPrefab;
    private Queue<GameObject> ballsQueue = new Queue<GameObject>();
    [SerializeField]
    float speed = 0.5f;


    public static ballSpawner Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        SetQueue();
    }

    private void SetQueue()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject instant = Instantiate(ballPrefab);
            instant.SetActive(false);
            ballsQueue.Enqueue(instant);
        }
    }

    public GameObject GetBall()
    {
        if (ballsQueue.Count == 0)
        {
            Debug.Log("No balls available!");
            return null;
        }

        GameObject ball = ballsQueue.Dequeue();
        ball.SetActive(true);
        return ball;
    }

    public void ReturnToPool(GameObject ball)
    {       
        ball.SetActive(false);
        ballsQueue.Enqueue(ball);
    }

    public void FireBall(GameObject enemy, Transform hedef)
    {
        GameObject ball = GetBall();
        if (ball == null) return;

        ball.transform.position = enemy.transform.position;
        ball.transform.rotation = Quaternion.identity;

        StartCoroutine(MoveBall(ball,hedef));
    }
    IEnumerator MoveBall(GameObject ball, Transform hedef)
    {
        // Her yeni top hareketi için sayacý sýfýrla
        float localCounter = 0f;

        while (ball != null && Vector3.Distance(ball.transform.position, hedef.position) > 0.11f)
        {
            ball.transform.position = Vector3.MoveTowards(ball.transform.position, hedef.position, speed * Time.deltaTime);

            localCounter += Time.deltaTime;
            if (localCounter >= 2f)
            {
                break;
            }
            yield return null;
        }

        if (ball != null)
        {
            ReturnToPool(ball);
        }
    }

}
