using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gui : MonoBehaviour
{
    public void loadscene()
    {
        SceneManager.LoadScene("Main VR Scene");
    }

    public void KeluarGame()
    {
        SceneManager.LoadScene("scene");
    }
}
