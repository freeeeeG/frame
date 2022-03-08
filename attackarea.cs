using UnityEngine;

public class attackarea : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<IDamageble>().GetHit(1);
        }
        if(collision.CompareTag("Boom"))
        {

        }
    }
    
}
