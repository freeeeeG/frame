using UnityEngine;

public class skill_attack_area : MonoBehaviour
{
    public baseSkill skill;
    private void OnTriggerEnter2D(Collider2D other)
    {

        Debug.Log(other.name);
        if (other.CompareTag("enemy"))
        {
            Debug.Log("111");
            other.GetComponent<IDamageble>().GetHit(skill.Atk);
            other.GetComponent<IDshake>().shake(transform);
        }

    }
}