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
        if(collision.tag == "Player" || collision.tag == "Enemy"){
            Health colHP = collision.gameObject.GetComponent<Health>();
            if(colHP != null){
                if(colHP.isEnemy == this.isEnemy) return; //if the bullet collides with somthing of the same alignment, ignore collision

                colHP.DealDamage(damage);
            
            }
            Destroy(this.gameObject);
        } else if(collision.tag == "Terrain")
            Destroy(this.gameObject);
    }
}
