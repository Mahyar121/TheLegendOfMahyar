using UnityEngine;
using System.Collections;

public class bookCaseII : MonoBehaviour {

	
	private void FixedUpdate () 
    {

        if (!GameObject.Find("DRAGONBOSS"))
        {
            Destroy(gameObject);
        }
	}
}
