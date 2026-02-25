using System;
using UnityEngine;
using UnityEngine.UIElements;

public class change_weapon_image : MonoBehaviour
    {
        [SerializeField]
        private UIDocument uiImage;
        private Image _img;
        
        private void Awake()
        {
            var root = uiImage.rootVisualElement;
            _img = root.Q<Image>("weaponImage");
        }
        
        
        private void OnEnable() =>  PlayerController.OnChangeImage += OnSpriteChange;

        private void OnDisable() => PlayerController.OnChangeImage -= OnSpriteChange;

        private void OnSpriteChange(Sprite sprite)
        {
            _img.sprite = sprite;
        }
        
    }
