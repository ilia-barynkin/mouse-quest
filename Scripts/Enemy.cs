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

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player").First();
        enemyCharacter = GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        enemyCharacter.MoveRelative(player.transform.position - transform.position);
        //enemyCharacter.FaceTo(Projection.FromScreenToScene(player.transform.position - transform.position));
        //enemyCharacter.FaceToScreenPoint((Vector2)player.transform.position);
    }
}
