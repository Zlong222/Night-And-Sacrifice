using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // ��������һ��SpriteRenderer���
    public SpriteRenderer targetRenderer;
    private SpritePathLoader pathLoader;

    private void Start()
    {
        Debug.Log(targetRenderer.sprite.texture.name);
    }
}
