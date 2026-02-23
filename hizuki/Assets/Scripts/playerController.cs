using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Weapon weapon;

    private void OnAttack(InputValue value)
    {
        if (value.isPressed) 
        {
            print("Pressed bateu");
             weapon.PerformAttack();
        }
    }
}