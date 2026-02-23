using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Collider2D weaponCollider;
    [SerializeField] private WeaponData weaponData;

    private void Start()
    {
        weaponCollider.enabled = false; // começa desligado
    }

    public void PerformAttack()
    {
      
        CancelInvoke(nameof(DisableCollider));

    
        weaponCollider.enabled = true;

        
        Invoke(nameof(DisableCollider), weaponData.attackSpeed);
    }

    private void DisableCollider()
    {
        weaponCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Só dá dano se colidir com inimigo
        if (collision.gameObject.layer != LayerMask.NameToLayer("enemy")) return;

        if (collision.TryGetComponent<IDamageable>(out var damageable))
        {
            damageable.TakeDamage(weaponData.damage);
            Debug.Log("Dano aplicado em: " + collision.name);
        }
    }
}