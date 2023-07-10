using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class CameraBehaviour : MonoBehaviour
{
    public GameObject watched;
    float initialZ;

    // Start is called before the first frame update
    void Start()
    {
        initialZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (watched != null) {
            transform.position = new Vector3(watched.transform.position.x, watched.transform.position.y, initialZ);
        }
    }
}
