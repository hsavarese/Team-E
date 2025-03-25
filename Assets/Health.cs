using UnityEngine;

public class Health : MonoBehaviour
{
    public float healthPoints;
    public bool isEnemy; //true for enemies, flase for allies
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(healthPoints <= 0){
            Destroy(this.gameObject);
        }
    }
}
