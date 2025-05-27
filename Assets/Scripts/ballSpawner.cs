using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballSpawner : MonoBehaviour
{
    public GameObject ballPrefab;
    private Queue<GameObject> ballsQueue = new Queue<GameObject>();
    [SerializeField] private float speed = 0.5f;
    public static ballSpawner Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        // Only initialize the queue if we have a valid prefab
        if (ballPrefab != null)
        {
            SetQueue();
        }
    }

    private void SetQueue()
    {
        if (ballPrefab == null)
        {
            Debug.LogError("Ball prefab is not set!");
            return;
        }

        for (int i = 0; i < 10; i++)
        {
            GameObject ball = Instantiate(ballPrefab);
            ball.SetActive(false);
            ballsQueue.Enqueue(ball);
        }
    }

    public GameObject GetBall()
    {
        if (ballsQueue.Count == 0) return null;
        GameObject ball = ballsQueue.Dequeue();
        ball.SetActive(true);
        return ball;
    }

    public void ReturnToPool(GameObject ball)
    {
        ball.SetActive(false);
        ballsQueue.Enqueue(ball);
    }

    public void FireBall(GameObject enemy, Transform target)
    {
        GameObject ball = GetBall();
        if (ball == null) return;

        ball.transform.position = enemy.transform.position;
        ball.transform.rotation = Quaternion.identity;
        StartCoroutine(MoveBall(ball, target));
    }

    private IEnumerator MoveBall(GameObject ball, Transform target)
    {
        float timer = 0f;
        while (ball != null && Vector3.Distance(ball.transform.position, target.position) > 0.11f)
        {
            ball.transform.position = Vector3.MoveTowards(ball.transform.position, target.position, speed * Time.deltaTime);
            timer += Time.deltaTime;
            if (timer >= 2f) break;
            yield return null;
        }
        if (ball != null) ReturnToPool(ball);
    }
}