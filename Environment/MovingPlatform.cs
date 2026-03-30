using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Waypoints")]
    public Transform pointA;
    public Transform pointB;

    [Header("Movement Settings")]
    public float speed = 0.5f;
    public float waitTime = 1;

    private bool waiting = false;
    private float timewaiting = 0;
    private bool movingTowardB = true;
    private float percentMoved = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = pointA.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (waiting)
        {
            Wait();
        }
        else
        {
            Move();
        }
    }

    private void Wait()
    {
        timewaiting += Time.deltaTime;
        if (timewaiting > waitTime)
        {
           timewaiting = 0;
           waiting= false;
        }
    }

    private void Move()
    {
        if (movingTowardB)
        {
            percentMoved+= Time.deltaTime * speed;
            if(percentMoved >= 1)
            {
                waiting=true;
                movingTowardB = false;
            }
        }
        else
        {
            percentMoved -= Time.deltaTime * speed;
            if (percentMoved <= 0)
            {
                waiting = true;
                movingTowardB = true;
            }
        }
        transform.position = Vector3.Lerp(pointA.position, pointB.position, percentMoved);
    }
}
