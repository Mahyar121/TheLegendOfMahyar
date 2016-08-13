using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Controls : MonoBehaviour {

    private GameObject[] controlObjects;

    private Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();

    public Text Up, Left, Down, Right, Jump, Climb, Attack, Menu; // have access to the text from unityengine.ui

    
	// Use this for initialization
	void Start () 
    {

        keys.Add("Up", KeyCode.UpArrow);
        keys.Add("Down", KeyCode.DownArrow);
        keys.Add("Left", KeyCode.LeftArrow);
        keys.Add("Right", KeyCode.RightArrow);
        keys.Add("Jump", KeyCode.Space);
        keys.Add("Climb", KeyCode.X);
        keys.Add("Attack", KeyCode.Z);
        keys.Add("Menu", KeyCode.Escape);

        Up.text = keys["Up"].ToString();
        Left.text = keys["Left"].ToString();
        Down.text = keys["Down"].ToString();
        Right.text = keys["Right"].ToString();
        Jump.text = keys["Jump"].ToString();
        Climb.text = keys["Climb"].ToString();
        Attack.text = keys["Attack"].ToString();
        Menu.text = keys["Menu"].ToString();
	}
	
	// Update is called once per frame
	public void LoadLevel()
    {
        if (SceneManager.GetActiveScene().name == "ControlPage")
        {
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            controlObjects = GameObject.FindGameObjectsWithTag("ShowControls");
            foreach (GameObject g in controlObjects)
            {
                g.SetActive(false);
            }
        }
    }
	
}
