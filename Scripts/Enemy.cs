using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Timeline;

public class Enemy : MonoBehaviour
{
    GameObject player;

    // TODO: rename component "Character"
    Character enemyCharacter;

    void Start() {
        enemyCharacter = GetComponent<Character>();
    }

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectsWithTag("Player").First();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null) {
            enemyCharacter.MoveRelative(player.transform.position - transform.position);
        }
    }
}
