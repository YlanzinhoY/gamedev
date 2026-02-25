using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Weapon _weapon;
    [SerializeField] private Transform weaponHolder;
    [SerializeField] public List<GameObject> weaponsPrefabs;
    
    public static event Action<Sprite> OnChangeImage;
    
    
    
    private int _weaponIndex = 0;
    private GameObject _currentWeapon;
    
    
    
    void Start()
    {
        EquipWeapon(_weaponIndex); 
    }

    private void OnAttack(InputValue value)
    {
        if (value.isPressed) 
        {
            print("Pressed bateu");
             _weapon.PerformAttack();
        }
    }

    private void OnRightWeapon(InputValue value)
    {
       if (value.isPressed)
       {
           NextWeapon();
       }
    }


    private void NextWeapon()
    {
        _weaponIndex++;

        if (_weaponIndex >= weaponsPrefabs.Count)
        {
            _weaponIndex = 0;
        }


        EquipWeapon(_weaponIndex);


    }

    private void EquipWeapon(int index)
    {
        if (_currentWeapon != null)
        {
            Destroy(_currentWeapon);
        }
        
        _currentWeapon = Instantiate(weaponsPrefabs[index], weaponHolder);
        
        
        _weapon = _currentWeapon.GetComponent<Weapon>();
        
        OnChangeImage?.Invoke(_weapon.weaponData.icon);
        
        Debug.Log($"Trocando de imagem ${OnChangeImage?.GetInvocationList()}");
        
        _currentWeapon.transform.localPosition = Vector2.zero;
        _currentWeapon.transform.localRotation = Quaternion.identity;
    }
    
}