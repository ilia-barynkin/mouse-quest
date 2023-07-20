using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    Character character;
    SpriteAnimation spriteAnimation;
    Attacker attacker;

    Health health;
    RectTransform lifebarRectTransform;

    // Start is called before the first frame update
    void Start()
    {
        spriteAnimation = GetComponent<SpriteAnimation>();
        character = GetComponent<Character>();
        attacker = GetComponent<Attacker>();
        health = GetComponent<Health>();

        // TODO: the most embarassing part

        var lb = GameObject.FindGameObjectsWithTag("Lifebar")[0];

        lifebarRectTransform = lb.GetComponent<RectTransform>();
    }

    const int HEART_WIDTH = 16;

    // Update is called once per frame
    void Update()
    {
        if (!character.busy) {
            character.MoveRelative(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"))); 

            if (Input.GetMouseButtonDown(0)) {
                attacker.Attack(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            }
        }

        // TODO: the most embarassing part

        lifebarRectTransform.SetSizeWithCurrentAnchors(0, HEART_WIDTH * health.currentHealth);

    }
}
