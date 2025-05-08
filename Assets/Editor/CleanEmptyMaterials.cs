using UnityEngine;
using UnityEditor;

public class CleanEmptyMaterials : MonoBehaviour
{
    [MenuItem("Tools/Clean Empty Material Slots (All Scene Objects)")]
    static void CleanAllMaterials()
    {
        int cleanedCount = 0;
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            Renderer renderer = obj.GetComponent<Renderer>();
            if (renderer != null)
            {
                Material[] oldMats = renderer.sharedMaterials;
                int validCount = 0;

                foreach (var mat in oldMats)
                    if (mat != null) validCount++;

                if (validCount < oldMats.Length)
                {
                    Material[] newMats = new Material[validCount];
                    int i = 0;
                    foreach (var mat in oldMats)
                        if (mat != null)
                            newMats[i++] = mat;

                    renderer.sharedMaterials = newMats;
                    cleanedCount++;
                }
            }
        }

        Debug.Log($"✅ Selesai: Membersihkan slot material kosong dari {cleanedCount} GameObject.");
    }
}
