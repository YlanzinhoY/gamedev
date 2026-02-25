using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Hit : MonoBehaviour
{
    [SerializeField]
    private UIDocument hitUIDocument;

    [SerializeField] private float timeBeforeHidden;
    
    private Weapon _weapon;
    private Label _damageTxt;
    private int rawDamage = 0;
    private Coroutine hideCoroutine;
    
    private void Awake()
    {
        var root = hitUIDocument.rootVisualElement;

        // Esconde no início
        root.style.visibility = Visibility.Hidden;
        
        _damageTxt = root.Q<Label>("damage");
    }
    

    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
   private void OnEnable() =>  Weapon.OnHitDamage += ShowDamage;

    private void OnDisable() => Weapon.OnHitDamage -= ShowDamage;

    private void ShowDamage(int damage)
    {
        Debug.Log($"damange: {damage}");
        
        rawDamage += damage;
        _damageTxt.text = rawDamage.ToString();
        
        var root = hitUIDocument.rootVisualElement;

        root.style.visibility = Visibility.Visible;
        
        
        if (hideCoroutine != null)
            StopCoroutine(hideCoroutine);

        StartCoroutine(HiddenAfterFinishDamage());
    }

    private IEnumerator HiddenAfterFinishDamage()
    {
        yield return new WaitForSeconds(timeBeforeHidden);
    
        rawDamage = 0;
        _damageTxt.text = rawDamage.ToString();
        hitUIDocument.rootVisualElement.style.visibility = Visibility.Hidden;

        hideCoroutine = null;
    }
}
