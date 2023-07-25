using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTouchDamage : MonoBehaviour
{
    Collider2D col;
    public float damage = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider2D>();
    }


    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate() {
        //var contacts = col.GetContacts;
    }

    void ApplyTo(Collider2D other) {
        var health = other.gameObject.GetComponent<Health>();
        
        if (health != null) {
            health.Hit(damage);
            var applied = other.ClosestPoint(transform.position) - (Vector2)transform.position;
            Debug.DrawLine((Vector2)transform.position,  (Vector2)transform.position + applied.normalized, Color.red);
            other.attachedRigidbody.AddForce(applied.normalized * 10.0f);
        }

        var playerControl = other.gameObject.GetComponent<PlayerControl>();
    }

    void OnTriggerEnter2D(Collider2D other) {
        ApplyTo(other);
    }
}
