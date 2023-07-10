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
    Vector2Int orient = new Vector2Int(0, -1);

    [field: SerializeField]
    Vector2 intendedMov = new Vector2();

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
        // else if (body.velocity.magnitude == 0.0f) { // TODO
        else {
            body.velocity = Vector2.zero; 
            spriteAnimation.SetDirAnim("Idle", orient);
        }

        intendedMov = Vector2.zero;
    }
}
