using UnityEngine;
using System.Collections;

public class backgroundMOVE : MonoBehaviour {

    [SerializeField]
    private Transform cam;
    [SerializeField]
    private float scrollSpeed;
    private Vector3 cameraOldPos;
    private Vector3 cameraCurrentPos;

    
    
   
	// Use this for initialization
	void Start () {

        cameraOldPos = cam.position;

       
	}
	
	// Update is called once per frame
	void Update () {

        cameraCurrentPos = cam.position;
        transform.position += (scrollSpeed *(cameraCurrentPos - cameraOldPos));
        cameraOldPos = cameraCurrentPos;
        
	
	}
}
