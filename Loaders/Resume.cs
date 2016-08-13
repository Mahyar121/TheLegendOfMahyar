using UnityEngine;
using System.Collections;

public class Resume : MonoBehaviour {

    private GameObject[] pauseObjects;

   
    public void OnClick()
    {
        pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
        Time.timeScale = 1;
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(false);
        }

    }
}
