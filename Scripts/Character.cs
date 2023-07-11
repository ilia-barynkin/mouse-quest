using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    SpriteAnimation spriteAnimation;
    Rigidbody2D body;
    int Discr(float x) { return x < 0.0f ? -1 : x > 0.0f ? 1 : 0; }
    Vector2Int GetDiscrVec(Vector2 vec) { return new Vector2Int(Discr(vec.x), Discr(vec.y)); }

    public string idleAnim = "Idle";
    public string runAnim = "Run";

    public bool busy = false;

    float speed = 1.0f;

    [field: SerializeField]
    Vector2Int orient = new Vector2Int(0, -1); // TODO ?

    [field: SerializeField]
    Vector2 intendedMov = new Vector2();

    public bool IsMoving {
        get {
            return intendedMov != Vector2.zero;
        }
    }

    const float ANGLE_ADJACENT = 45.0f / 2.0f;

    public Vector2Int FaceTo(Vector2 vec) {
        var relVec = vec - (Vector2)transform.position;

        var middleAxis = Mathf.Abs(relVec.x) > Mathf.Abs(relVec.y) ? 
            new Vector2(Mathf.Sign(relVec.x), 0.0f) : new Vector2(0.0f, Mathf.Sign(relVec.y));
        
        if (Vector2.Angle(relVec.normalized, middleAxis) <= ANGLE_ADJACENT) {
            orient = new Vector2Int((int)middleAxis.x, (int)middleAxis.y);
        }
        else {
            orient = GetDiscrVec(relVec);
        }

        return orient;
    }

    public void Move(Vector2 dir) {
        if (dir != Vector2.zero) {
            intendedMov = dir;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        spriteAnimation = GetComponent<SpriteAnimation>();
    }

    public void Stop() {
        intendedMov = Vector2.zero;
        body.velocity = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (!busy) {
            if (intendedMov != Vector2.zero) {
                body.velocity = intendedMov.normalized * speed;
                orient = GetDiscrVec(intendedMov);
                spriteAnimation.Play(runAnim, orient);
            }
            else {
                body.velocity = Vector2.zero; 
                spriteAnimation.Play(idleAnim, orient);
            }
        }

        intendedMov = Vector2.zero;
    }
}
