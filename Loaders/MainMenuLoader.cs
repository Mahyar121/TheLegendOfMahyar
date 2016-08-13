using UnityEngine;
using System.Collections;

public class MainMenuLoader : MonoBehaviour {

    public void LoadLevel()
    {

        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");

    }
}
