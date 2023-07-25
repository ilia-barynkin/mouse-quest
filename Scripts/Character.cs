using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    SpriteAnimation spriteAnimation;
    Rigidbody2D body;

    public string idleAnim = "Idle";
    public string runAnim = "Run";

    public bool busy = false;

    public float speed = 1.0f;

    [field: SerializeField]
    Vector2 intendedMov = new Vector2();

    public bool IsMoving {
        get {
            return intendedMov.magnitude > 0.0f;
        }
    }

    public void MoveRelative(Vector2 dir) {
        if (!busy) {
            intendedMov = dir;
        }
    }

    Orient orient;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        spriteAnimation = GetComponent<SpriteAnimation>();
        orient = GetComponent<Orient>();
    }

    public void Stop() {
        intendedMov = Vector2.zero;
        body.velocity = Vector2.zero;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!busy) {
            if (intendedMov != Vector2.zero) {
                body.velocity = intendedMov.normalized * speed;
                orient.FaceToScenePoint(intendedMov);
                // TODO TODO TODO
                spriteAnimation.Play(runAnim, orient.screenVec);
            }
            else {
                body.velocity = Vector2.zero; 
                spriteAnimation.Play(idleAnim, orient.screenVec);
            }
        }

        intendedMov = Vector2.zero;
    }
}
