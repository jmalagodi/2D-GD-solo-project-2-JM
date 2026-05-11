using UnityEngine;

public class MovingLight : MonoBehaviour
{
    public Vector3 pointA;
    public Vector3 pointB;

    public float minSpeed = 1f;
    public float maxSpeed = 4f;

    public float changeDirectionInterval = 1.5f;
    public float pauseChance = 0.2f; // 20% chance to pause when changing behavior
    public float pauseTimeMin = 0.1f;
    public float pauseTimeMax = 1.2f;

    private Vector3 target;
    private float speed;

    private float behaviorTimer;
    private bool isPaused;
    private float pauseTimer;

    void Start()
    {
        target = pointB;
        speed = Random.Range(minSpeed, maxSpeed);
        behaviorTimer = Random.Range(0.5f, changeDirectionInterval);
    }

    void Update()
    {
        // Handle pause
        if (isPaused)
        {
            pauseTimer -= Time.deltaTime;
            if (pauseTimer <= 0f)
            {
                isPaused = false;
                PickNewBehavior();
            }
            return;
        }

        // Move smoothly toward current target
        transform.position = Vector3.MoveTowards(
            transform.position,
            target,
            speed * Time.deltaTime
        );

        // Timer for random behavior changes (NOT endpoint-based)
        behaviorTimer -= Time.deltaTime;
        if (behaviorTimer <= 0f)
        {
            DecideRandomBehavior();
            behaviorTimer = Random.Range(0.5f, changeDirectionInterval);
        }
    }

    void DecideRandomBehavior()
    {
        // Random pause anywhere in motion
        if (Random.value < pauseChance)
        {
            isPaused = true;
            pauseTimer = Random.Range(pauseTimeMin, pauseTimeMax);
            return;
        }

        // Random speed change
        speed = Random.Range(minSpeed, maxSpeed);

        // Random direction flip OR new target interpolation
        target = (Random.value < 0.5f) ? pointA : pointB;
    }

    void PickNewBehavior()
    {
        speed = Random.Range(minSpeed, maxSpeed);
        target = (Random.value < 0.5f) ? pointA : pointB;
    }
}