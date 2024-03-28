using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Projectile : MonoBehaviour
{
    public float lifetime = 5f;
    public float speed = 100f;
    private int damage;

    public void SetProjectileProperties(int damage)
    {
        this.damage = damage;
        StartCoroutine(DestroyAfterLifetime());
    }

    IEnumerator DestroyAfterLifetime()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }

    void Update()
    {
        transform.Translate(-Vector3.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        MovingTarget movingTarget = other.GetComponent<MovingTarget>();
        if (movingTarget != null)
        {
            movingTarget.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
