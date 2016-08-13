using UnityEngine;
using System.Collections;

public class PlatformMoveUpDown : MonoBehaviour {


    [SerializeField]
    private Transform[] Points;
    [SerializeField]
    private float speed;
   
    private int CurrentPoint = 0;
  
    
    private void Update()
    {
        
        if (transform.position.y != Points[CurrentPoint].position.y)
        {
            transform.position = Vector3.MoveTowards(transform.position, Points[CurrentPoint].position, (Time.deltaTime * speed));
        }
        if (transform.position.y == Points[CurrentPoint].position.y)
        {
            CurrentPoint += 1;
        }
        if ( CurrentPoint >= Points.Length)
        {
            CurrentPoint = 0;
        }
        
           
    }

  
    
}
