using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class lvl3BossCredits : MonoBehaviour {

	private void FixedUpdate()
    {
        if (!GameObject.Find("EvilSchoolTeacher"))
        {
            SceneManager.LoadScene("Credits");
        }
    }
}
