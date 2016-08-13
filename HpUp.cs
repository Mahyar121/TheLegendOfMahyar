using UnityEngine;
using System.Collections;

public class HpUp : MonoBehaviour {

    

    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            
            Destroy(gameObject);
        }
    }
	

}
