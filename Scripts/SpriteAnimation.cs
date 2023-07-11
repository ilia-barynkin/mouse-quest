using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;

[System.Serializable]
public class SpriteSeq: List<Sprite> {
    // TODO ?
    // public float framesPerSec;

    // public SpriteSeq(IEnumerable<Sprite> sprites, float framesPerSec) : base(sprites) { }
}

[System.Serializable]
public class OrientedAnimations: GenericDictionary<Vector2Int, List<Sprite>> {}

[System.Serializable]
public class OrientedAnimationsBucket: GenericDictionary<string, OrientedAnimations> {}

[System.Serializable]
public class SpriteAnimation : MonoBehaviour
{
    [SerializeField]
    public GenericDictionary<string, GenericDictionary<Vector2Int, AnimationClip>> animations;

    [HideInInspector]
    [SerializeField]
    public OrientedAnimationsBucket animsBucket;
    
    [SerializeField]
    SpriteRenderer spriteRenderer;

    Stack<string> animStack = new Stack<string>();
    public float framesPerSec = 12.0f;

    // TODO
    float animDelta = 0.0f;
    float frameDuration;
    float AnimLoopDuration {
        get {
            return CurrentSpriteSeq.Count * frameDuration;
        }
    }

    public string CurrAnimName {
        get {
            const string fallback = "Idle"; // TODO
            var res = animStack.FirstOrDefault();
            return String.IsNullOrEmpty(res) ? fallback : res;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //animStack.Push("Attack"); // TODO: kostyl
        frameDuration = 1.0f / framesPerSec;
        spriteRenderer = GetComponent<SpriteRenderer>();

        UpdateSprite(0);
    }

    List<Sprite> CurrentSpriteSeq {get {return animsBucket[CurrAnimName][animDir]; }}
    Vector2Int animDir = new Vector2Int(0, -1);

    // Update is called once per frame
    void Update()
    {
        animDelta += Time.deltaTime;

        if (animDelta >= AnimLoopDuration) {
            if(animStack.Count > 1) {
                animStack.Pop();
                animDelta = 0.0f;
            }
            else 
                animDelta -= AnimLoopDuration;
        }
        
        int frameIndex = Mathf.FloorToInt(Mathf.Lerp(0.0f, CurrentSpriteSeq.Count, animDelta / AnimLoopDuration));

        UpdateSprite(frameIndex);
    }

    void UpdateSprite(int frameId) {
        spriteRenderer.sprite = CurrentSpriteSeq[frameId];
    }

    public void Play(string animName, Vector2Int dir, bool looped = true) {
        if (animName == CurrAnimName && dir == animDir) {
            return;
        }
        
        if (animsBucket.ContainsKey(animName)) {
            if (looped) animStack.Clear(); // un-looped animations actually go to stack
            animStack.Push(animName);
            animDir = dir;
            animDelta = 0.0f;
        }
        else {
            Debug.Log("Animation name doesn't exist: " + animName);
        }
    }
}
