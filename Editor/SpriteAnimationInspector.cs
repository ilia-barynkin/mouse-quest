using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;

// provide the component type for which this inspector UI is required
[CustomEditor(typeof(SpriteAnimation))]
public class SpriteAnimationEditor : Editor
{
    List<Sprite> GetSpriteSeq(AnimationClip clip) {
        try {
            var binding = AnimationUtility.GetObjectReferenceCurveBindings(clip).First(x => 
                x.propertyName == "m_Sprite");

            var res = AnimationUtility.GetObjectReferenceCurve(clip, binding).Select(x => x.value as Sprite).ToList();

            return res;
        }
        catch (Exception e) {
            Debug.Log("Sprite binding: " + e.ToString());
            
            throw new Exception();
        }
    }

    bool animationsCreated = false;

    public override void OnInspectorGUI()
    {
        // TODO: Serialization, changes
        if (GUILayout.Button("GenerateAnimations")) {
            var t = target as SpriteAnimation;
            t.animsBucket = new OrientedAnimationsBucket();
            
            foreach (var currAnimChunk in t.animations) {
                var orientAnims = new OrientedAnimations();

                foreach (var item in currAnimChunk.Value) { 
                    orientAnims[item.Key] = GetSpriteSeq(item.Value);
                }

                t.animsBucket.Add(currAnimChunk.Key, orientAnims);
            }

            EditorUtility.SetDirty(target);
            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
            animationsCreated = true;
        }

        GUILayout.Label("Animations " + (!animationsCreated ? "NOT " : "") + "created");

        // will enable the default inpector UI 
        base.OnInspectorGUI();
    }
}