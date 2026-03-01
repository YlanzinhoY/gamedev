using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace script.Controller
{
    public class GameController : MonoBehaviour
    {

        [SerializeField] private UIDocument scoreboardUi;
        [SerializeField] private UIDocument retryUI;
        [SerializeField] private Sprite winSprite;
        [SerializeField] private Sprite loseSprite;
        [SerializeField] private Dice dice;

        private Image _scorePoint;
        private Label _retryLabel;
        private int _rounded = 1;
        private int? _point;
        private int _retry = 6;

        private List<int> _scoreMemory = new List<int>();
    

        private void Awake()
        {
            var mainContainer = scoreboardUi.rootVisualElement;
            _scorePoint =  mainContainer.Q<Image>($"rodada-{_rounded}");
            
            _retryLabel = retryUI.rootVisualElement.Q<Label>("retry");
            _retryLabel.text = (_retry - 1).ToString();

            _retry = 5;
        }


        private void OnEnable() => Dice.OnGameStatus += GameManager;
        private void OnDisable() => Dice.OnGameStatus -= GameManager;


        void GameManager(int sum, string gameCondition)
        {
            FinalGameCheck();
            
            if (TurnController.IsPlayerPlaying)
            {
                Game(sum, gameCondition);

                if (_retry <= 0)
                {
                    Debug.Log("Player terminou tentativas");

                    _retry = 5;
                    _retryLabel.text = _retry.ToString();

                    TurnController.IsPlayerPlaying = false;
                    TurnController.IsCpuAlive = true;
                    _point = null;
                    TurnController._canRoll = false;
                    dice.CpuPlay();
                }
            }
            else if (TurnController.IsCpuAlive)
            {
                Game(sum, gameCondition);

                if (_retry <= 0)
                {
                    Debug.Log("CPU terminou tentativas");

                    _retry = 5;
                    _retryLabel.text = _retry.ToString();
                    _point = null;
                    TurnController._canRoll = true;
                    TurnController.IsCpuAlive = false;
                    TurnController.IsPlayerPlaying = true;
                }
                else
                {
                    dice.CpuPlay();
                }
            }
        }
        
        void DecreaseAttempt()
        {
            _retry--;
            _retryLabel.text = _retry.ToString();
        }
        
        void Game(int sum, string gameCondition)
        {
            var container = scoreboardUi.rootVisualElement;
            var image = container.Q<Image>($"rodada-{_rounded}");

            
            Debug.Log($"Sum: {sum}");
            Debug.Log($"Condition: {gameCondition}");
            Debug.Log($"memory {_scoreMemory.Count}");

            if (_point == null)
            {
                
                if (gameCondition == "win")
                {
                    if (TurnController.IsPlayerPlaying)
                    {
                        image.sprite = winSprite;
                        _rounded++;
                        _scoreMemory.Add(1);
                        FinalGameCheck();
                        return;
                    }

                    if (TurnController.IsCpuAlive)
                    {
                        image.sprite = loseSprite;
                        _scoreMemory.Add(0);
                        _rounded++;
                        FinalGameCheck();
                    }
                }
                else if (gameCondition == "lose")
                {
                    if (TurnController.IsPlayerPlaying)
                    {
                        image.sprite = loseSprite;
                        _rounded++;
                        _scoreMemory.Add(0);
                        FinalGameCheck();
                        return;
                    }
                    
                    if (TurnController.IsCpuAlive)
                    {
                        image.sprite = winSprite;
                        _rounded++;
                        _scoreMemory.Add(1);
                        FinalGameCheck();
                    }
                }
                else if (gameCondition == "point")
                {
                    _point = sum;
                    Debug.Log($"Point definido: {_point}");
                }
                
                return;
            }
            
            
            if (_point != null)
            {
                DecreaseAttempt();
                if (sum == _point)
                {
                    if (TurnController.IsPlayerPlaying)
                    {
                        image.sprite = winSprite;
                        _rounded++;
                        _point = null;
                        _scoreMemory.Add(1);
                        FinalGameCheck();
                        return;
                    }

                    if (TurnController.IsCpuAlive)
                    {
                        image.sprite = loseSprite;
                        _point = null;
                        _rounded++;
                        _scoreMemory.Add(0);
                        FinalGameCheck();
                    }
                }
                else if (sum == 7)
                {
                    if (TurnController.IsPlayerPlaying)
                    {
                        image.sprite = loseSprite;
                        _point = null;
                        _rounded++;
                        _scoreMemory.Add(0);
                        FinalGameCheck();
                        return;
                    }
                    
                    if (TurnController.IsCpuAlive)
                    {
                        image.sprite = winSprite;
                        _point = null;
                        _rounded++;
                        _scoreMemory.Add(1);
                        FinalGameCheck();
                    }
                }
            }
        }

        void FinalGameCheck()
        {

            var win = _scoreMemory.Count(x => x == 1) >= 3;
            var lose = _scoreMemory.Count(x => x == 0) >= 3;

            if (win)
            {
                TurnController.IsPlayerPlaying = false;
                TurnController.IsCpuAlive = false;
                Time.timeScale = 0f;
                Debug.Log($"Final Game: you WIN");
                return;
            }

            if (lose)
            {
                TurnController.IsPlayerPlaying = false;
                TurnController.IsCpuAlive = false;
                Time.timeScale = 0f;
                Debug.Log("Final Game game: You lose");
            }
            
        }

    }
}