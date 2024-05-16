using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LeanTween;

public class GateBehaviour : MonoBehaviour
{
    public PressurePlateBehaviour pressPlate;
    Vector2 initPos;

    void Start()
    {
        initPos = transform.position;
    }

    void Update()
    {
        if (pressPlate.pressed || pressPlate.boxPressed)
        {
            StartCoroutine(MoveGateUp());
        }
        else
        {
            StartCoroutine(MoveGateDown());
        }
    }

    IEnumerator MoveGateUp()
    {
        Vector2 newPos = initPos + Vector2.up * 1.5f;
        newPos.y = Mathf.Clamp(newPos.y, initPos.y, initPos.y + 1.5f);
        moveLocalY(gameObject, newPos.y, 0.3f).setEase(LeanTweenType.easeOutSine);
        yield return new WaitForSeconds(0.3f);
    }

    IEnumerator MoveGateDown()
    {
        yield return new WaitForSeconds(0.3f);
        moveLocalY(gameObject, initPos.y, 0.3f).setEase(LeanTweenType.easeOutSine);
        yield return new WaitForSeconds(0.3f);
    }
}
