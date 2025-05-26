using UnityEngine;

public class MockCollision : Collision
{
    public new GameObject gameObject { get; set; }
}

public class MockTypeWrite : MonoBehaviour
{
    public static MockTypeWrite Instance { get; private set; }
    public bool isStoryFinished = true;

    private void Awake()
    {
        Instance = this;
    }
}
