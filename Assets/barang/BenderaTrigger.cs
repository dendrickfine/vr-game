using UnityEngine;

public class BenderaTrigger : MonoBehaviour
{
    public Transform posisiKibar;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bendera"))
        {
            other.transform.position = posisiKibar.position;
            other.transform.rotation = posisiKibar.rotation;
        }
    }
}
