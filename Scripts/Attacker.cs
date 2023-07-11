using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    Character character;
    SpriteAnimation spriteAnimation;

    public string attackAnimName = "Attack";
    public string attackIdleAnimName = "AttackIdle";
    public float delayAfterAttack = 1.0f;

    float delayDelta = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<Character>();
        spriteAnimation = GetComponent<SpriteAnimation>();
    }

    string idleAnimNameBefore = "";

    public void Attack(Vector2 point) {
        delayDelta = delayAfterAttack; // always lower than the actual attack animation

        // TODO: replace with indirect call from character, maybe
        spriteAnimation.Play("Attack", character.FaceTo(point), false);
        idleAnimNameBefore = character.idleAnim;
        character.idleAnim = attackIdleAnimName;
        character.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (delayDelta > 0.0f) {
            character.busy = true;
            delayDelta -= Time.deltaTime;
        }

        if (delayDelta <= 0.0f) {
            delayDelta = 0.0f;
            character.busy = false;

            if (character.IsMoving) {
                character.idleAnim = idleAnimNameBefore;
            }
        }
    }
}
