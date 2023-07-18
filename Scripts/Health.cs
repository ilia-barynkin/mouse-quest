using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float healthMax = 3.0f;
    public float currentHealth = 0.0f;

    // Start is called before the first frame update
    void Awake()
    {
        currentHealth = healthMax;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0.0f) {
            Destroy(gameObject);
        }
    }
}
