using TMPro;
using UnityEngine;

public class QuestTracker : MonoBehaviour
{
    public TextMeshProUGUI questText;
    private bool[] itemStatus = new bool[4];

    void Start()
    {
        UpdateQuestText();
    }

    public void MarkItemFound(int index)
    {
        if (index < 0 || index >= itemStatus.Length) return;
        itemStatus[index] = true;
        UpdateQuestText();
    }

    void UpdateQuestText()
    {
        questText.text =
            "Kondisi Menang:\n" +
            (itemStatus[0] ? "Selesai" : "☐") + " Temukan Foto Presiden Pertama Indonesia!\n" +
            (itemStatus[1] ? "Selesai" : "☐") + " Temukan Bendara Indonesia!\n" +
            (itemStatus[2] ? "Selesai" : "☐") + " Temukan Rekaman Proklamasi!\n" +
            (itemStatus[3] ? "Selesai" : "☐") + " Temukan Buku UUD 1945!\n\n" +
            "Kondisi Kalah:\n" +
            "Waktu habis";
    }
}
