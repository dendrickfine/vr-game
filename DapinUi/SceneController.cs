using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // Load scene utama (misal saat "Mulai" ditekan)
    public void LoadMainScene()
    {
        SceneManager.LoadScene("Main VR Scene");
    }

    // Load scene untuk menu opsi
    public void LoadOptions()
    {
        SceneManager.LoadScene("Options");
    }

    // Kembali ke menu utama
    public void LoadMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    // Keluar dari aplikasi (hanya bekerja jika dibuild, bukan di editor)
    public void QuitGame()
    {
        Debug.Log("Keluar dari game...");
        Application.Quit();
    }
}
