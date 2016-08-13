using UnityEngine;
using System.Collections;

public class BossSight : MonoBehaviour {


    [SerializeField]
    private Boss1 boss;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            boss.Target = other.gameObject;

        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            boss.Target = null;

        }
    }
}
