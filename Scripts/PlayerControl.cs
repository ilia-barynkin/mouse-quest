using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    Character character;
    SpriteAnimation spriteAnimation;

    // Start is called before the first frame update
    void Start()
    {
        spriteAnimation = GetComponent<SpriteAnimation>();
        character = GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        character.Move(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));

        if (Input.GetMouseButton(0)) {
            character.FaceTo(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            
        }
    }
}
