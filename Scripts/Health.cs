using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float healthMax = 3.0f;
    public float currentHealth = 0.0f;

    SpriteRenderer spriteRenderer;

    Color spriteColorBeforeHit;

    // Start is called before the first frame update
    void Awake()
    {
        currentHealth = healthMax;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public float invincibilityTimeAfterHit = 0.0f;

    public float invincible = 0.0f;

    public bool isInvincible {
        get {return invincible > 0.0f;}
    }

    bool isHit = false;

    public void Hit(float damage) {
        if (invincible == 0.0f) {
            isHit = true;
            spriteColorBeforeHit = spriteRenderer.color;
            spriteRenderer.color = Color.red;
            invincible = invincibilityTimeAfterHit;
            currentHealth -= damage;

            if (currentHealth <= 0.0f) {
                Destroy(gameObject);
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isHit) {
            isHit = false;
            spriteRenderer.color = spriteColorBeforeHit;
        }

        if (invincible >= 0.0f) {
            invincible = Mathf.Max(0.0f, invincible - Time.deltaTime);
        }


    }
}
