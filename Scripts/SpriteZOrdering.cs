using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpriteZOrdering : MonoBehaviour
{
    public float zBase = -0.5f;
    public float ratioYZ = 0.3f;

    float minY = 0;

    // TODO
    public GameObject topPoint;


    // Start is called before the first frame update
    void Start()
    {
        if (topPoint != null) {
            minY = topPoint.transform.position.y;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 
            zBase - (minY - transform.position.y) * ratioYZ);
    }
}
