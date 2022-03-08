using UnityEngine;

public class ArrowShot : MonoBehaviour
{
    // Start is called before the first frame update
    float speed =0.05f;
    float atk = 1;
    public GameObject a;
        void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = transform.position+new Vector3(-1*0.5f*speed,0,0);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<IDamageble>().GetHit(atk);
            Destroy(a);
        }
    }
}
