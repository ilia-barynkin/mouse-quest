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

    float speed = 1.0f;

    [field: SerializeField]
    Vector2Int orient = new Vector2Int(0, -1); // TODO ?

    [field: SerializeField]
    Vector2 intendedMov = new Vector2();

    const float ANGLE_ADJACENT = 45.0f / 2.0f;

    public void FaceTo(Vector2 vec) {
        var relVec = vec - (Vector2)transform.position;

        var middleAxis = Mathf.Abs(relVec.x) > Mathf.Abs(relVec.y) ? 
            new Vector2(Mathf.Sign(relVec.x), 0.0f) : new Vector2(0.0f, Mathf.Sign(relVec.y));
        
        if (Vector2.Angle(relVec.normalized, middleAxis) <= ANGLE_ADJACENT) {
            orient = new Vector2Int((int)middleAxis.x, (int)middleAxis.y);
        }
        else {
            orient = GetDiscrVec(relVec);
        }
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

    // Update is called once per frame
    void Update()
    {
        if (intendedMov != Vector2.zero) {
            body.velocity = intendedMov.normalized * speed;
            orient = GetDiscrVec(intendedMov);
            spriteAnimation.SetDirAnim("Run", orient);
        }
        else {
            body.velocity = Vector2.zero; 
            spriteAnimation.SetDirAnim("Idle", orient);
        }

        intendedMov = Vector2.zero;
    }
}
