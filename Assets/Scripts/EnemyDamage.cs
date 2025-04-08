using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public float damage;
    public bool isEnemy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        Health colHP = collision.gameObject.GetComponent<Health>();
        if(colHP != null){
            if(colHP.isEnemy == this.isEnemy) return; //if the collision is between two things of the same alignment, ignore collision

            colHP.DealDamage(damage);
        }
    }

}
