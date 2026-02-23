using UnityEngine;


public enum WeaponType
{
    Melee,
    Ranged,
    Magic
}


[CreateAssetMenu(fileName = "WeaponData", menuName = "Weapon/New Weapon")]
public class WeaponData : ScriptableObject
{

    [Header("Basic information")] 
    public string weaponName; 
    public WeaponType  weaponType;
    public Sprite icon;
    
    [Header("Status")]
    public int damage;
    public float range;
    public float attackSpeed;
    
    [Header("Optional")]
    public GameObject projectilePrefab;
    public AudioClip attackSound;
}
