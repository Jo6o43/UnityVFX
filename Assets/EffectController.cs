using UnityEngine;
using System.Collections;
using UnityEngine.VFX;


public class EffectController : MonoBehaviour
{
    [Header("Effect Settings")]
    [SerializeField] public VisualEffect effect1;
    [SerializeField] public float effect1_spawnRate;
    [SerializeField] public VisualEffect effect2;
    [SerializeField] public float effect2_spawnRate;
    [SerializeField] public GameObject blueParticles;
    [SerializeField] public GameObject blueSparks;
    [SerializeField] public float timeBetween = 3.0f;
    [SerializeField] public float duration = 5.0f;

    [Header("Camera Controller")]
    [SerializeField] public Transform camTransform;
    [SerializeField] public float shakeAmount = 0.4f;
    [SerializeField] public float decreaseFactor = 1.0f;
    Vector3 originalPos;
    private bool cameraShake = false;

    void Start()
    {
        if (effect1 != null) effect1_spawnRate = effect1.GetFloat("SpawnRate");
        if (effect2 != null) effect2_spawnRate = effect2.GetFloat("SpawnRate");

        if (effect1 != null) effect1.SetFloat("SpawnRate", 0f);
        if (effect2 != null) effect2.SetFloat("SpawnRate", 0f);

        if (blueParticles != null) blueParticles.SetActive(false);
        if (blueSparks != null) blueSparks.SetActive(false);


        camTransform = GetComponent(typeof(Transform)) as Transform;
        originalPos = camTransform.localPosition;

        Debug.Log("Start -> E  //  Stop -> R  //  Play Event -> Q");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(Activate());
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Desactivate());
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(Event());
        }

        if (cameraShake)
        {
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
        }
    }

    IEnumerator Activate()
    {
        Debug.Log("Start Effect");
        if (effect1 != null)
        {
            effect1.SetFloat("SpawnRate", effect1_spawnRate);
        }

        yield return new WaitForSeconds(timeBetween);

        if (effect2 != null)
        {
            effect2.SetFloat("SpawnRate", effect2_spawnRate);
        }

        if (blueParticles != null)
        {
            blueParticles.SetActive(true);
        }

        if (blueSparks != null)
        {
            blueSparks.SetActive(true);
        }
        
        cameraShake = true;
    }

    IEnumerator Desactivate()
    {
        Debug.Log("Stop Effect");
        if (effect1 != null)
        {
            effect1.SetFloat("SpawnRate", 0f);
        }

        yield return new WaitForSeconds(timeBetween);

        if (effect2 != null)
        {
            effect2.SetFloat("SpawnRate", 0f);
        }

        if (blueParticles != null)
        {
            blueParticles.SetActive(false);
        }

        if (blueSparks != null)
        {
            blueSparks.SetActive(false);
        }

        yield return new WaitForSeconds(1.0f);

        cameraShake = false;
    }

    IEnumerator Event()
    {
        Debug.Log("Start Event");
        StartCoroutine(Activate());
        yield return new WaitForSeconds(duration);
        StartCoroutine(Desactivate());
        Debug.Log("End Event");
    }
}