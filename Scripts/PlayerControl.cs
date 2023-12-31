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
    Collider2D col;

    // Start is called before the first frame update
    void Start()
    {
        spriteAnimation = GetComponent<SpriteAnimation>();
        character = GetComponent<Character>();
        attacker = GetComponent<Attacker>();
        health = GetComponent<Health>();
        col = GetComponent<Collider2D>();

        // TODO: the most embarassing part

        var lb = GameObject.FindGameObjectsWithTag("Lifebar")[0];

        lifebarRectTransform = lb.GetComponent<RectTransform>();
    }

    const int HEART_WIDTH = 16;

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetMouseButtonDown(0)) {
        //     attacker.Attack(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        // }
        // if (!character.busy) {
        //     character.MoveRelative(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"))); 

        //     if (Input.GetMouseButtonDown(0)) {
        //         attacker.Attack(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        //     }
        // }

        // TODO: the most embarassing part

        lifebarRectTransform.SetSizeWithCurrentAnchors(0, HEART_WIDTH * health.currentHealth);
    }

    List<Collider2D> collidedWith = new List<Collider2D>();

    public float touchDamage = 0.5f;

    void FixedUpdate() {
        if (Input.GetMouseButtonDown(0)) {
            attacker.Attack(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
        character.MoveRelative(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"))); 

        // if (Input.GetMouseButtonDown(0)) {
        //     attacker.Attack(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        // }
        // collision with enemies themselves
        // if (!health.isInvincible && col.GetContacts(collidedWith) > 0) {
        //     foreach(var c in collidedWith) {
        //         var enemy = c.gameObject.GetComponent<Enemy>();
        //         if (enemy != null) {
        //             health.Hit(touchDamage);
        //         }
        //     }
        // }
    }

    void LateUpdate() {
        // if (Input.GetMouseButtonDown(0)) {
        //     attacker.Attack(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        // }
    }
}
