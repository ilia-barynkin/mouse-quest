using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ThrustProjectile : MonoBehaviour
{
    public float damage = 1.0f;
    Collider2D coll;
    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collider2D>();
        coll.enabled = false;
    }

    float lifeTime = 0.0f;

    // Update is called once per frame
    void Update()
    {
        if ((lifeTime += Time.deltaTime) >= timeToActivate) {
            coll.enabled = true;
        }
    }

    public float timeToActivate = 0.2f;

    HashSet<int> enemiesTouched = new HashSet<int>();

    // public void SwitchCollider(bool isOn = true) {
    //     if (!isOn) 
    //         enemiesTouched.Clear();
            
    //     coll.enabled = isOn;
    // }

    void OnTriggerEnter2D(Collider2D other) {
        if (!enemiesTouched.Contains(other.gameObject.GetInstanceID())) {
            enemiesTouched.Add(other.gameObject.GetInstanceID());
            Debug.Log(enemiesTouched.Count);//other.gameObject.GetInstanceID());
            var health = other.gameObject.GetComponent<Health>();
            if (health != null) {
                health.currentHealth -= damage;
                var applied = other.ClosestPoint(transform.position) - (Vector2)transform.position;
                Debug.DrawLine((Vector2)transform.position,  (Vector2)transform.position + applied.normalized, Color.red);
                other.attachedRigidbody.AddForce(applied.normalized * 10.0f);
            }
        }
    }

}
