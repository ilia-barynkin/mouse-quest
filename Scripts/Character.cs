using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WASDOrient {
    W, A, S, D, WA, AS, SD, DW
};

public struct AnimNameOriented {
    public string animName;
    //WASDOrient orient;
    public Vector2Int orient;

    public override int GetHashCode() {
        return animName.GetHashCode() + orient.GetHashCode();
    }
};

public class AnimBucket {
    public Dictionary<AnimNameOriented, AnimationState> animations  = new Dictionary<AnimNameOriented, AnimationState>();

    // "BS" stands for Best suitable, of course
    public AnimBucket(Animation animBS) {
        foreach (AnimationState animState in animBS) {
            var lastTwoLetters = animState.name.Trim()[^2..];

            Vector2Int orient =  new Vector2Int(
                lastTwoLetters.Contains('W') ? 1 : lastTwoLetters.Contains('S') ? -1 : 0,
                lastTwoLetters.Contains('A') ? 1 : lastTwoLetters.Contains('D') ? -1 : 0
            );

            var animTypeName = animState.name.Trim()[..^(orient.x + orient.y)];

            animations.Add(new AnimNameOriented{ animName = animTypeName, orient = orient }, animState);
        }
    }
}

public class Character : MonoBehaviour
{
    public Rigidbody2D body;
    int Discr(float x) { return x < 0.0f ? -1 : x > 0.0f ? 1 : 0; }
    // intended movement
    Vector2Int mov;
    AnimBucket animBucket;
    Animation animations;

    void PlayAnim(string animName, Vector2Int orient) {
        animations..Play(animBucket.animations[new AnimNameOriented(animName, orient)]];
    };

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animBucket = new AnimBucket(GetComponent<Animation>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
