using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    Rigidbody2D body;
    Animator animator;

    // TODO: replace speed
    float speed = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.Play("Idle");
    }

    int Discr(float x) { return x < 0.0f ? -1 : x > 0.0f ? 1 : 0; }

    // Update is called once per frame
    void Update()
    {
        body.velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized * speed;
        animator.SetInteger("X", Discr(body.velocity.x));
        animator.SetInteger("Y", Discr(body.velocity.y));

        Debug.Log("X: " + Discr(body.velocity.x).ToString() + "; Y: " + Discr(body.velocity.y).ToString());

    }

    // private void OnCollisionEnter2D(Collision2D col) {
    //     col.contacts[0].normal 

    // }

    // private void OnTriggerEnter(Collider other)
    // {
    //     //speed = -1.0f;
    //     Debug.Log("OnTriggerEnter");
    // }
}
