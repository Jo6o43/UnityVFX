using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public Transform camTransform;

    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;

    Vector3 originalPos;

    void Awake()
    {
        if (camTransform == null)
        {
            camTransform = GetComponent(typeof(Transform)) as Transform;
        }
    }

    void OnEnable()
    {
        originalPos = camTransform.localPosition;
    }

    void Update()
    {

        camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

    }
}