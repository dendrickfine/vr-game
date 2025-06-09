using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlikeringLight : MonoBehaviour
{
    public Light spotLight;          // Assign via Inspector (or will auto-grab)
    public float minFlickerDelay = 0.05f; // Min time between flickers
    public float maxFlickerDelay = 0.2f;  // Max time between flickers
    public bool randomIntensity = true;
    public float minIntensity = 0.3f;
    public float maxIntensity = 1.0f;

    private void Start()
    {
        if (spotLight == null)
        {
            spotLight = GetComponent<Light>();
        }

        if (spotLight != null)
        {
            StartCoroutine(Flicker());
        }
    }

    private System.Collections.IEnumerator Flicker()
    {
        while (true)
        {
            // Toggle light on/off or randomize intensity
            if (randomIntensity)
            {
                spotLight.intensity = Random.Range(minIntensity, maxIntensity);
            }
            else
            {
                spotLight.enabled = !spotLight.enabled;
            }

            float waitTime = Random.Range(minFlickerDelay, maxFlickerDelay);
            yield return new WaitForSeconds(waitTime);
        }
    }
}
