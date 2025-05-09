using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public GameObject fotoSoekarno;
    public Transform piguraPos;
    public GameObject soalPanel;
    public Text soalText;

    private bool isFotoDiambil = false;
    private bool isFotoDipasang = false;

    private string[] soalSejarah = {
        "Siapa Presiden pertama Indonesia?",
        "Kapan Indonesia merdeka?"
    };

    void Update()
    {
        if (isFotoDiambil && !isFotoDipasang && Vector3.Distance(fotoSoekarno.transform.position, piguraPos.position) < 1f)
        {
            PasangFoto();
        }
    }

    public void AmbilFoto()
    {
        isFotoDiambil = true;
    }

    void PasangFoto()
    {
        isFotoDipasang = true;
        fotoSoekarno.transform.position = piguraPos.position;
        fotoSoekarno.transform.rotation = piguraPos.rotation;

        TampilkanSoal();
    }

    void TampilkanSoal()
    {
        soalPanel.SetActive(true);
        soalText.text = soalSejarah[Random.Range(0, soalSejarah.Length)];
    }
}
