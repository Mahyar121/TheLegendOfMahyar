using UnityEngine;
using System.Collections;

public class schoolDoor : MonoBehaviour {

    private Player person;

	private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            
        }
    }
}
