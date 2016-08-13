using UnityEngine;
using System.Collections;

public class bookCase : MonoBehaviour {

	
	private void FixedUpdate () 
    {
        if (!GameObject.Find("SkeletonKing"))
        {
            Destroy(gameObject);
        }
	
	}
}
