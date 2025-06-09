using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class SceneController : MonoBehaviour
{
    // Nama scene utama
    private string mainSceneName = "devit";
    private string optionSceneName = "devit"; // kalau nanti beda, tinggal ubah

    // Hapus EventSystem lama (yang mungkin terbawa dari scene sebelumnya)
    private void CleanUpBeforeLoad()
    {
        EventSystem[] eventSystems = FindObjectsOfType<EventSystem>();
        if (eventSystems.Length > 1)
        {
            // Hapus EventSystem yang bukan milik scene baru
            for (int i = 0; i < eventSystems.Length; i++)
            {
                if (eventSystems[i].gameObject.scene.name != SceneManager.GetActiveScene().name)
                {
                    Destroy(eventSystems[i].gameObject);
                }
            }
        }

        // Hapus object dengan tag tertentu jika pakai DontDestroyOnLoad
        GameObject persistent = GameObject.Find("DontDestroyOnLoad");
        if (persistent != null)
        {
            Destroy(persistent);
        }
    }

    public void LoadMainScene()
    {
        if (SceneManager.GetActiveScene().name != mainSceneName)
        {
            CleanUpBeforeLoad();
            SceneManager.LoadScene(mainSceneName);
        }
    }

    public void LoadOptions()
    {
        if (SceneManager.GetActiveScene().name != optionSceneName)
        {
            CleanUpBeforeLoad();
            SceneManager.LoadScene(optionSceneName);
        }
    }

    public void LoadMenu()
    {
        if (SceneManager.GetActiveScene().name != "MainMenu")
        {
            CleanUpBeforeLoad();
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void QuitGame()
    {
        Debug.Log("Keluar dari game...");
        Application.Quit();
    }
}
