using UnityEngine;

public class MovingTarget : MonoBehaviour
{  
    public float speed = 5f;
    public float maxDistance = 10f;

    private Vector3 initialPosition;
    private float movementDirection = 1f;

    public float maxHealth = 100f;
    public float currentHealth;

    public float disableTime = 5f;
    private float disableTimer;

    void Start()
    {
        currentHealth = maxHealth;
        initialPosition = transform.position;
    }

    void Update()
    {
        transform.Translate(Vector3.right * movementDirection * speed * Time.deltaTime);

        if (Mathf.Abs(transform.position.x - initialPosition.x) >= maxDistance)
        {
            movementDirection *= -1f;
        }

        float clampedX = Mathf.Clamp(transform.position.x, initialPosition.x - maxDistance, initialPosition.x + maxDistance);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("Target Hit!");
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            DisableTarget();
        }
    }

    private void DisableTarget()
    {
        Debug.Log("Target Down!");
        gameObject.SetActive(false);
        disableTimer = disableTime;
        InvokeRepeating("RegenerateHealth", 0f, 1f);
    }

    private void RegenerateHealth()
    {
        disableTimer -= 1f;

        if (disableTimer <= 0f)
        {
            Debug.Log("Target Back Up!");
            CancelInvoke("RegenerateHealth");
            currentHealth = maxHealth;
            gameObject.SetActive(true);
        }
    }
    public float GetCurrentHealth(){return currentHealth;}
    public bool IsAlive(){
        if(!(currentHealth == 0))
            return true;
        else
            return false;
    }
}