using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public GameObject critter;

    List<GameObject> critters = new List<GameObject>();
    // Start is called before the first frame update
    void Awake()
    {
        //critters.Add(Instantiate(critter, transform.position, Quaternion.identity));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) {
            //if (critters.Count <= maxRespawned)
            critters.Add(Instantiate(critter, transform.position, Quaternion.identity));
        }
    }
}
