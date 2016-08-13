using UnityEngine;
using System.Collections;

public class PlatformMoveRightLeft : MonoBehaviour {

    [SerializeField]
    private Transform[] Points;
    [SerializeField]
    private float speed;

    private int CurrentPoint = 0;


    private void Update()
    {

        if (transform.position.x != Points[CurrentPoint].position.x)
        {
            transform.position = Vector3.MoveTowards(transform.position, Points[CurrentPoint].position, (Time.deltaTime * speed));
        }
        if (transform.position.x == Points[CurrentPoint].position.x)
        {
            CurrentPoint += 1;
        }
        if (CurrentPoint >= Points.Length)
        {
            CurrentPoint = 0;
        }


    }

}
