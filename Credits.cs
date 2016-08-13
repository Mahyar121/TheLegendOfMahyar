using UnityEngine;
using System.Collections;

public class Credits : MonoBehaviour {

   
    private float speed = 1;
    private float time = 12;
	// Update is called once per frame
    
	void Update () 
    {


        transform.Translate(Vector3.down * Time.deltaTime * speed);
        time -= Time.deltaTime;
            
        if(time < 0)
        {
           
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        }
             
	}

    
        
 }