using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    Character character;
    SpriteAnimation spriteAnimation;

    public string attackAnimName = "Attack";
    public string attackIdleAnimName = "AttackIdle";
    public float delayAfterAttack = 1.0f;

    float delayDelta = 0.0f;

    public GameObject projectile;
    Orient orient;

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<Character>();
        spriteAnimation = GetComponent<SpriteAnimation>();
        orient = GetComponent<Orient>();
    }

    public void Attack(Vector2 point) {
        delayDelta = delayAfterAttack; // always lower than the actual attack animation

        // TODO: replace with indirect call from character, maybe
        spriteAnimation.Play("Attack", orient.FaceToScreenPoint(point), false);
        character.idleAnim = attackIdleAnimName;
        idleAnimChanged = true;
        character.Stop();

        projectiles.Push(Instantiate(projectile, transform.position, Quaternion.identity));
    }

    Stack<GameObject> projectiles = new Stack<GameObject>();

    bool idleAnimChanged = false;

    // Update is called once per frame
    void Update()
    {
        if (delayDelta > 0.0f) {
            character.busy = true;
            delayDelta -= Time.deltaTime;

            if (delayDelta <= 0.0f) {
                delayDelta = 0.0f;
                character.busy = false;
                Destroy(projectiles.Pop());
            }
        }

        if (idleAnimChanged && character.IsMoving) {
            character.idleAnim = "Idle"; // TODO
            idleAnimChanged = false;
        }
    }
}
