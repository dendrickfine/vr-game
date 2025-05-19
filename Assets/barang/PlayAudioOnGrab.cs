using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayAudioOnGrab : MonoBehaviour
{
    public AudioSource audioSource;

    void Start()
    {
        XRGrabInteractable grab = GetComponent<XRGrabInteractable>();
        grab.selectEntered.AddListener((args) => {
            if (!audioSource.isPlaying)
                audioSource.Play();
        });
    }
}
