using System;
using UnityEngine;

public class Projection {
    public const float angleX = 45.0f;

    public static Vector2 FromScreenToScene(Vector2 screenRel) {
        return new Vector2(screenRel.x, screenRel.y / Mathf.Cos(angleX));
    }

    public static Vector2 FromSceneToScreen(Vector2 sceneRel) {
        return new Vector2(sceneRel.x, sceneRel.y * Mathf.Cos(angleX));
    }
}