using System;
using System.Collections;
using script;
using script.Controller;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class Dice : MonoBehaviour
{
    
    [SerializeField]
    public Sprite[] sprites;
    
    [SerializeField] private GameObject diceFace1;
    [SerializeField] private GameObject diceFace2;
    [SerializeField, Range(1,20)] private int  throwForce;

    public static event Action<int, string> OnGameStatus;
    
    
    private int _finalDiceFace;
    private int[] _playerRollDice;
    private SpriteRenderer diceFaceSprite1;
    private SpriteRenderer diceFaceSprite2;

    
    private int _randomDiceSide;
    private int _randomDiceSide2;

    private void Awake()
    {
        diceFaceSprite1 = diceFace1.GetComponent<SpriteRenderer>();
        diceFaceSprite1.sprite = sprites[0];
        
        diceFaceSprite2 = diceFace2.GetComponent<SpriteRenderer>();
        diceFaceSprite2.sprite = sprites[0];
    }
    

    public void OnRoll(InputAction.CallbackContext context)
    {
        if(!TurnController._canRoll) return;
        if (!context.performed) return;

        StartCoroutine(nameof(RollTheDice));
        
        Debug.Log($"Fire");
    }

    private IEnumerator RollTheDice()
    {

        for (var i = 0; i <= throwForce; i++)
        {
            _randomDiceSide = Random.Range(0, sprites.Length);
            _randomDiceSide2 =  Random.Range(0, sprites.Length);
            
            diceFaceSprite1.sprite = sprites[_randomDiceSide];
            diceFaceSprite2.sprite = sprites[_randomDiceSide2];
            
            yield return new WaitForSeconds(0.10f);
            
        }
        
        var sum =  (_randomDiceSide + 1) + (_randomDiceSide2 + 1);
        
        var craps = new Craps();

        var state = craps.GameState(sum);
        
        OnGameStatus?.Invoke(sum, state);
        
        Debug.Log(sum);
        Debug.Log(state);

    }
    
    public void CpuPlay()
    {
        
        Debug.Log("CPU está jogando...");
        TurnController.IsCpuAlive = true;
        
        StartCoroutine(nameof(CpuRoll));
    }

    private IEnumerator CpuRoll()
    {
        yield return new WaitForSeconds(1.5f);
        yield return DiceLogic();
    }

    private IEnumerator DiceLogic()
    {
        for (var i = 0; i <= throwForce; i++)
        {
            _randomDiceSide = Random.Range(0, sprites.Length);
            _randomDiceSide2 = Random.Range(0, sprites.Length);

            diceFaceSprite1.sprite = sprites[_randomDiceSide];
            diceFaceSprite2.sprite = sprites[_randomDiceSide2];

            yield return new WaitForSeconds(0.10f);
        }
        
        var sum = (_randomDiceSide + 1) + (_randomDiceSide2 + 1);

        var craps = new Craps();
        var state = craps.GameState(sum);
        

        OnGameStatus?.Invoke(sum, state);
    }
    
}
