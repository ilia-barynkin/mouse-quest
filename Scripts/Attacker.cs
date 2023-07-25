using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    Character character;
    SpriteAnimation spriteAnimation;
    Orient orient;

    public string attackAnimName = "Attack";
    public string attackIdleAnimName = "AttackIdle";
    public float delayAfterAttack = 1.0f;

    float delayDelta = 0.0f;

    public GameObject projectile;
    Rigidbody2D body;

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<Character>();
        spriteAnimation = GetComponent<SpriteAnimation>();
        orient = GetComponent<Orient>();
        body = GetComponent<Rigidbody2D>();
    }

    public bool isAttacking {
        get {
            return delayDelta > 0.0f;
        }
    }

    public float dashRange = 0.4f;

    public void Attack(Vector2 point) {
        if (!character.busy && !isAttacking) {
            delayDelta = delayAfterAttack; // always lower than the actual attack animation

            // TODO: replace with indirect call from character, maybe
            orient.FaceToScreenPoint(point);
            spriteAnimation.Play("AttackIdle", orient.screenVec);
            spriteAnimation.Play("Attack", orient.screenVec, false);
            character.idleAnim = attackIdleAnimName;
            idleAnimChanged = true;
            character.Stop();

            projectiles.Push(Instantiate(projectile, transform.position + orient.sceneVec * range, Quaternion.identity));
        }
    }

    Stack<GameObject> projectiles = new Stack<GameObject>();

    bool idleAnimChanged = false;
    float range = 0.4f;

    void FixedUpdate() {
        if (isAttacking) {
            body.velocity = orient.sceneVec * dashRange * 
                (Mathf.Max(delayDelta, Mathf.Epsilon) / delayAfterAttack);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: add some 
        if (isAttacking) {
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
