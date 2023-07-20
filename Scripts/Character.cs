using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    SpriteAnimation spriteAnimation;
    Rigidbody2D body;
    // int Discr(float x) { return x < 0.0f ? -1 : x > 0.0f ? 1 : 0; }
    // Vector2Int GetDiscrVec(Vector2 vec) { return new Vector2Int(Discr(vec.x), Discr(vec.y)); }

    public string idleAnim = "Idle";
    public string runAnim = "Run";

    public bool busy = false;

    public float speed = 1.0f;

    // TODO TODO TODO : figure out something
    // [field: SerializeField]
    // Vector2Int orient = new Vector2Int(0, -1); // TODO ?

    [field: SerializeField]
    Vector2 intendedMov = new Vector2();

    public bool IsMoving {
        get {
            // Debug.Log("intendedMov = " + intendedMov.ToString());
            // Debug.Log("intendedMov.x > 0.0f || intendedMov.y > 0.0f" + (intendedMov.x > 0.0f || intendedMov.y > 0.0f));
            //return intendedMov.x > 0.0f || intendedMov.y > 0.0f;

            return body.velocity.magnitude > Mathf.Epsilon;
        }
    }

    // const float ANGLE_ADJACENT = 45.0f / 2.0f;

    // TODO TODO TODO

    // public Vector2Int FaceToRelScreenPoint(Vector2 relVecScreen) { 
    //     var relVec = Projection.FromScreenToScene(relVecScreen);
    //     var middleAxis = Mathf.Abs(relVec.x) > Mathf.Abs(relVec.y) ? 
    //         new Vector2(Mathf.Sign(relVec.x), 0.0f) : new Vector2(0.0f, Mathf.Sign(relVec.y));
        
    //     if (Vector2.Angle(relVec.normalized, middleAxis) <= ANGLE_ADJACENT) {
    //         orient = new Vector2Int((int)middleAxis.x, (int)middleAxis.y);
    //     }
    //     else {
    //         orient = GetDiscrVec(relVec);
    //     }

    //     return orient;
    // }

    // public Vector2Int FaceToScreenPoint(Vector2 screenPoint) {
    //     return FaceToRelScreenPoint(screenPoint - (Vector2)transform.position);
    // }

    public void MoveRelative(Vector2 dir) {
        if (dir != Vector2.zero) {
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
    void Update()
    {
        if (!busy) {
            if (intendedMov != Vector2.zero) {
                body.velocity = intendedMov.normalized * speed;
                // TODO TODO TODO
                spriteAnimation.Play(runAnim, orient.FaceToRelScreenPoint(intendedMov));
            }
            else {
                body.velocity = Vector2.zero; 
                spriteAnimation.Play(idleAnim, orient.screenVec);
            }
        }

        // var sceneOrient = Projection.FromSceneToScreen(orient);

        // Debug.DrawLine(new Vector3(transform.position.x, transform.position.y, 0.0f),
        //     new Vector3(transform.position.x + sceneOrient.x, transform.position.y + sceneOrient.y, 0.0f));

        intendedMov = Vector2.zero;
    }
}
