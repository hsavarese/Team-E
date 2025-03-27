using UnityEngine;

public class BullletDamage : MonoBehaviour
{
    public float damage;
    public bool isEnemy; //true for enemy bullets, false for ally bullets

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Health colHP = collision.gameObject.GetComponent<Health>();
        if(colHP != null){
            if(colHP.isEnemy == this.isEnemy) return; //if the bullet collides with somthing of the same alignment, ignore collision

            colHP.healthPoints -= damage;
            Destroy(this.gameObject);
        }
    }
}
