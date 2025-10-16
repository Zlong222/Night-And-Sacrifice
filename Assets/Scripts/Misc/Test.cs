using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // 假设已有一个SpriteRenderer组件
    public SpriteRenderer targetRenderer;
    private SpritePathLoader pathLoader;

    private void Start()
    {
        Debug.Log(targetRenderer.sprite.texture.name);
    }
}
