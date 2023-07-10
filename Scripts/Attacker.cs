using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    Character character;
    SpriteAnimation spriteAnimation;

    public string attackAnimName = "Attack";
    public float delayAfterAttack = 1.0f;

    float delayDelta = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<Character>();
    }

    public void Attack(Vector2 point) {
        character.FaceTo(point);
        delayDelta = delayAfterAttack;
    }

    // Update is called once per frame
    void Update()
    {
        if (delayDelta > 0.0f) {
            character.Busy = true;
            delayDelta -= Time.deltaTime;

            if (delayDelta <= 0.0f) {
                delayDelta = 0.0f;
                character.Busy = false;
            }
        }
    }
}
