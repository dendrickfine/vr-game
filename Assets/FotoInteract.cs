using UnityEngine;

public class FotoInteract : MonoBehaviour
{
    public QuestManager questManager;

    void OnMouseDown()
    {
        // VR: Ganti dengan input controller (misalnya trigger)
        questManager.AmbilFoto();
        Debug.Log("Foto diambil!");

        // Simulasi: foto diangkat oleh pemain
        transform.position = Camera.main.transform.position + Camera.main.transform.forward * 1f;
    }
}
