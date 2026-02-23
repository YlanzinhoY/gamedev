using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{

    public int life;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (life <= 0)
        {
            Destroy(gameObject);
        }
        
    }

    public void TakeDamage(int damage)
    {
        life -= damage;
        Debug.Log(damage);
    }
}
