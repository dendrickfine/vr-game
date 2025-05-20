using UnityEngine;

public class PintuAuto : MonoBehaviour
{
    public BukaTutupPintu skripPintu;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            skripPintu.isOpen = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            skripPintu.isOpen = false;
        }
    }
}
