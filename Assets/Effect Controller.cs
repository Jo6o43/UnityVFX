using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor.XR;

public class EffectController : MonoBehaviour
{
    [Header("Effect Settings")]
    [SerializeField] public GameObject effect1;
    [SerializeField] public GameObject effect2;
    [SerializeField] public float timeBetween = 3.0f;

    [Header("Camera Controller")]
    [SerializeField] public Transform camTransform;
    [SerializeField] public float shakeAmount = 0.7f;
    [SerializeField] public float decreaseFactor = 1.0f;
    Vector3 originalPos;
    private bool cameraShake = false;

    void Start()
    {
        effect1.SetActive(false);
        effect2.SetActive(false);
    }

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
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(Activate());
        }

        if (cameraShake)
        {
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
        }
    }

    IEnumerator Activate()
    {
        if (effect1 != null)
        {
            effect1.SetActive(true);
        }

        yield return new WaitForSeconds(timeBetween);

        if (effect2 != null)
        {
            effect2.SetActive(true);
        }
        cameraShake = true;
    }
}