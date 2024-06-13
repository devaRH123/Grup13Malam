using System;
using UnityEngine;

public class BackgroundRepeater : MonoBehaviour
{
    float Speed = 0.0005f;
    float offset;
    Material mat;
    float playerPos;
    float initPos;

    void Start()
    {
        mat = GetComponent<Renderer>().material;
        mat.SetTextureOffset("_MainTex", new Vector2(Speed * (initPos + playerPos) / 10, 0));
        initPos = transform.position.x;
    }

    void Update()
    {
        playerPos = GameObject.Find("Player").transform.position.x;
        offset = Speed * (initPos + playerPos);
        mat.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }
}
