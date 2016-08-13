using UnityEngine;
using System.Collections;

public class Ladder : MonoBehaviour, IUseable {

    [SerializeField]
    private Collider2D platformCollider;

    

    public void Use()
    {
        if (Player.Instance.OnLadder)
        {
            UseLadder(false, 1, 1, false); // stop climbing
            
            
        }
        else
        {
            UseLadder(true, 0,  0, true); // start climbing
            
            Physics2D.IgnoreCollision(Player.Instance.GetComponent<Collider2D>(), platformCollider, true); // ignore collision between two objects
        }


    }

    private void UseLadder(bool onLadder, int gravity, int animSpeed, bool land)
    {
        Player.Instance.OnLadder = onLadder;
        Player.Instance.MyRigidbody.gravityScale = gravity;
        Player.Instance.MyAnimator.speed = animSpeed;
        Player.Instance.MyAnimator.SetBool("climbing", land);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            UseLadder(false, 1, 1, false); // stop climbing
            Player.Instance.MyAnimator.SetLayerWeight(0, 1);
            Physics2D.IgnoreCollision(Player.Instance.GetComponent<Collider2D>(), platformCollider, false); // ignore collision between two objects
        }
    }
}
