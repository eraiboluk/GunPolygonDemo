using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public MovingTarget movingTarget;

    private Transform healthBarTransform;
    private float originalScale;

    private void Start()
    {
        healthBarTransform = transform;
        originalScale = healthBarTransform.localScale.x;
    }

    private void LateUpdate()
    {
        if (movingTarget.IsAlive())
        {
            float healthPercent = movingTarget.GetCurrentHealth() / movingTarget.maxHealth;
            healthBarTransform.localScale = new Vector3(originalScale * healthPercent, 0.01f, 0.00001f);
        }
        else
        {
            healthBarTransform.localScale = Vector3.zero;
        }
    }
}
