using UnityEngine;

public class BukaTutupPintu : MonoBehaviour
{
    public bool isOpen = false;
    public float speed = 2f;
    private Quaternion targetRotation;

    void Start()
    {
        targetRotation = transform.rotation;
    }

    void Update()
    {
        Quaternion desiredRotation = isOpen ? Quaternion.Euler(0, 90, 0) : Quaternion.Euler(0, 0, 0);
        targetRotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * speed);
        transform.rotation = targetRotation;
    }

    public void TogglePintu()
    {
        isOpen = !isOpen;
    }
}
