using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ControlLoader : MonoBehaviour
{

    private GameObject[] controlObjects;

    private void Start()
    {
        controlObjects = GameObject.FindGameObjectsWithTag("ShowControls");
        hideControls();
    }
    public void LoadLevel()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            SceneManager.LoadScene("ControlPage");
        }
        else
        {
            showControls();
        }
    }

    private void showControls()
    {
        foreach (GameObject g in controlObjects)
        {
            g.SetActive(true);
        }
    }

    private void hideControls()
    {
        foreach (GameObject g in controlObjects)
        {
            g.SetActive(false);
        }
    }
}
