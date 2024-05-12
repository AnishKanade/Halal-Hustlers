using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float jumIter = 4.5f;

    private void Update()
    {
        if(Input.GetKeyDown("c"))
        {
            Shake();
        }
    }

    public void Shake()
    {
        float height = Mathf.PerlinNoise(jumIter, 0f) * 5;
        height = height * height * 0.2f;

        float shakeAmt = height; // degrees to shake the camera
        float shakePeriodTime = 0.25f; // period of each shake
        float dropOffTime = 1.25f; // how long it takes to settle down to nothing

        LTDescr shakeTween = LeanTween
            .rotateAroundLocal(gameObject, Vector3.right, shakeAmt, shakePeriodTime)
            .setEase(LeanTweenType.easeShake)
            .setLoopClamp()
            .setRepeat(-1);

        LeanTween.value(gameObject, shakeAmt, 0, dropOffTime)
            .setOnUpdate((float val) =>
            {
                shakeTween.setTo(Vector3.right * val);
            })
            .setEase(LeanTweenType.easeOutQuad);
    }
}
