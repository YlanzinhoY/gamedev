using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Collider2D weaponCollider;
    [SerializeField] public WeaponData weaponData;
    public static event Action<int> OnHitDamage;

    private void Awake()
    {
       
        if (weaponCollider == null)
            weaponCollider = GetComponentInChildren<Collider2D>();

        weaponCollider.enabled = false;
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
      
        if (collision.gameObject.layer != LayerMask.NameToLayer("enemy")) return;

        if (collision.TryGetComponent<IDamageable>(out var damageable))
        {
            damageable.TakeDamage(weaponData.damage);
            Debug.Log("Dano aplicado em: " + collision.name);
            
            
            OnHitDamage?.Invoke(weaponData.damage);
            Debug.Log($"Disparando evento. Listeners: {OnHitDamage?.GetInvocationList().Length}");
            
        }
    }
}