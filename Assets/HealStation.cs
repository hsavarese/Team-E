using UnityEngine;

public class HealStation : MonoBehaviour
{
    public float heal;
    public float totalLifetime;
    public float healTime;
    public bool isEnemy;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(this.gameObject, totalLifetime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Player" || collision.tag == "Enemy"){
            Health colHP = collision.gameObject.GetComponent<Health>();
            if(colHP != null){
                if(colHP.isEnemy != this.isEnemy) return; //if the bullet collides with somthing of the same alignment, ignore collision

                Destroy(this.gameObject, healTime);
            }
        } 
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player" || collision.tag == "Enemy"){
            Health colHP = collision.gameObject.GetComponent<Health>();
            if(colHP != null){
                if(colHP.isEnemy != this.isEnemy) return; //if the bullet collides with somthing of the same alignment, ignore collision

                colHP.RestoreHealth(heal);
            }
        } 
    }
}

