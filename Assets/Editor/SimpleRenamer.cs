using UnityEngine;
using UnityEditor;

public class SimpleRenamer : MonoBehaviour
{
    [MenuItem("Tools/Rename to Sequential Object Names")]
    static void RenameAllToSequentialTables()
    {
        GameObject[] selectedObjects = Selection.gameObjects;

        if (selectedObjects.Length == 0)
        {
            Debug.LogWarning("⚠️ Pilih dulu objek-objek yang ingin direname di Hierarchy.");
            return;
        }

        // Urutkan sesuai urutan di hierarchy (bukan berdasarkan nama)
        System.Array.Sort(selectedObjects, (a, b) => 
            a.transform.GetSiblingIndex().CompareTo(b.transform.GetSiblingIndex()));

        for (int i = 0; i < selectedObjects.Length; i++)
        {
            Undo.RecordObject(selectedObjects[i], "Rename Objects");
            selectedObjects[i].name = $"chair-{i + 1}";
        }

        Debug.Log("✅ Semua objek berhasil dinamai ulang sebagai table1, table2, dst.");
    }
}