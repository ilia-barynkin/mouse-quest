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

    public string currAnimName = "Idle";
    public float framesPerSec = 12.0f;

    // TODO
    float animDelta = 0.0f;
    float frameDuration;
    float animLoopDuration;

    // Start is called before the first frame update
    void Start()
    {
        frameDuration = 1.0f / framesPerSec;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animLoopDuration = CurrentSpriteSeq.Count * frameDuration;

        UpdateSprite(0);
    }

    List<Sprite> CurrentSpriteSeq {get {return animsBucket[currAnimName][animDir]; }}

    //Sprite CurrentSprite {get {return CurrentSpriteSeq[currFrameId];} }

    Vector2Int animDir = new Vector2Int(0, -1);

    // Update is called once per frame
    void Update()
    {
        // TODO : loop

        animDelta += Time.deltaTime;
        if (animDelta >= animLoopDuration) {
            animDelta -= animLoopDuration;
            // if ((currFrameId += 1) >= CurrentSpriteSeq.Count())  a
            //     currFrameId -= CurrentSpriteSeq.Count();
        }

        
        int frameIndex = Mathf.FloorToInt(Mathf.Lerp(0.0f, CurrentSpriteSeq.Count, animDelta / animLoopDuration));
        Assert.IsTrue(frameIndex >= 0 && frameIndex < CurrentSpriteSeq.Count);
        UpdateSprite(frameIndex);
        


        // animDelta += Time.deltaTime;
        // if (animDelta >= frameDuration) {
        //     animDelta -= frameDuration;

        //     if (currFrameId++ >= CurrentSpriteSeq.Count())
        //         currFrameId = 0;

        //     UpdateSprite();
        // }
    }

    void UpdateSprite(int frameId) {
        spriteRenderer.sprite = CurrentSpriteSeq[frameId];
    }

    public void SetDirAnim(string animName, Vector2Int dir) {
        if (animName == currAnimName && dir == animDir) {
            return;
        }
        
        if (animsBucket.ContainsKey(animName)) {
            //Debug.Log("SetDirAnim(" + animName + ", " + dir.ToShortString() + ")");
            currAnimName = animName;
            animDir = dir;
            animDelta = 0.0f;
            animLoopDuration = CurrentSpriteSeq.Count * frameDuration;
        }
        else {
            Debug.Log("Animation name doesn't exist: " + animName);
        }
    }
}
