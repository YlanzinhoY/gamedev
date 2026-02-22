using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Transformation : MonoBehaviour
{
    
   
    [SerializeField]
    GameObject Player;
   
   [SerializeField]
   Sprite []sprite;

   [SerializeField] private float transformationTime;
   private float normalSpeed;
   
   private SpriteRenderer spritePlayer;
   private Coroutine transformationCoroutine;
   private Moviment moviment;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spritePlayer = Player.GetComponent<SpriteRenderer>();
        
        
        moviment = Player.GetComponent<Moviment>();
        
        normalSpeed = moviment.speed;
    }

    // Update is called once per frame
    void Update()
    {
      
        
       
    }

    private void OnTransformation(InputValue value)
    {
        if (value.isPressed)
        {
            
            if (transformationCoroutine != null)
                StopCoroutine(transformationCoroutine);

            transformationCoroutine = StartCoroutine(TransformRoutine());
        }
    }
    
    
    private IEnumerator TransformRoutine()
    {
       
        spritePlayer.sprite = sprite[1];
        moviment.speed = normalSpeed * 1.25f;
       
        yield return new WaitForSeconds(transformationTime);

       
        spritePlayer.sprite = sprite[0];
        moviment.speed = normalSpeed;
    }
}
