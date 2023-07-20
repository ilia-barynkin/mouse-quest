using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orient : MonoBehaviour
{
    static int Discr(float x) { return x < 0.0f ? -1 : x > 0.0f ? 1 : 0; }
    
    static Vector2Int GetDiscrVec(Vector2 vec) { return new Vector2Int(Discr(vec.x), Discr(vec.y)); }

    const float ANGLE_ADJACENT = 45.0f / 2.0f;

    public Vector2Int screenVec = new Vector2Int(0, -1); // TODO ?

    public void FaceToScenePoint(Vector2 relVecScreen) { 
        var relVec = Projection.FromScreenToScene(relVecScreen);
        var middleAxis = Mathf.Abs(relVec.x) > Mathf.Abs(relVec.y) ? 
            new Vector2(Mathf.Sign(relVec.x), 0.0f) : new Vector2(0.0f, Mathf.Sign(relVec.y));
        
        if (Vector2.Angle(relVec.normalized, middleAxis) <= ANGLE_ADJACENT) {
            screenVec = new Vector2Int((int)middleAxis.x, (int)middleAxis.y);
        }
        else {
            screenVec = GetDiscrVec(relVec);
        }

        //return screenVec;
    }

    public Vector3 sceneVec{
        get {
            return Projection.FromSceneToScreen(screenVec);
        }
    }

    public void FaceToScreenPoint(Vector2 screenPoint) {
        FaceToScenePoint(screenPoint - (Vector2)transform.position);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(new Vector3(transform.position.x, transform.position.y, 0.0f),
            new Vector3(transform.position.x + sceneVec.x, transform.position.y + sceneVec.y, 0.0f));        
    }
}
