using UnityEngine;
using System.Collections;

public class PlayLoader : MonoBehaviour 
{
    public void LoadLevel()
    {

        UnityEngine.SceneManagement.SceneManager.LoadScene("Level1");

    }
	
}
