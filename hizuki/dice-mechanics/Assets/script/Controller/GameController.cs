using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using script.Service;
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
        [SerializeField] private int _retry;

        private Image _scorePoint;
        private Label _retryLabel;
        private int _rounded = 1;
        private int? _point;
        

        private List<int> _scoreMemory = new();
        private ITurnStateService _currentTurn;
    

        private void Awake()
        {
            var mainContainer = scoreboardUi.rootVisualElement;
            
            _scorePoint =  mainContainer.Q<Image>($"rodada-{_rounded}");
            
            _retryLabel = retryUI.rootVisualElement.Q<Label>("retry");

            _retryLabel.text = _retry.ToString();

            _currentTurn = new PlayerController();
        }

        private void OnEnable() => Dice.OnGameStatus += GameManager;
        private void OnDisable() => Dice.OnGameStatus -= GameManager;

        private void GameManager(int sum, MatchState? matchState)
        {
            if(matchState != null)
                _currentTurn.Play(this, sum,  matchState);
            FinalGameCheck();
        }
        
        public void ResolveRoll(int sum, MatchState? result, bool isPlayerTurn)
        {
            var image = scoreboardUi.rootVisualElement.Q<Image>($"rodada-{_rounded}");
            var point = retryUI.rootVisualElement.Q<Label>("point");

            if (_point == null)
            {
                switch (result)
                {
                    case MatchState.Win:
                        FinishRound(image, point, isPlayerTurn, true);
                        break;

                    case MatchState.Lose:
                        FinishRound(image, point, isPlayerTurn, false);
                        break;

                    case MatchState.Point:
                        _point = sum;
                        point.text = $"Point: {_point}";
                        break;
                    case null:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(result), result, null);
                }

                return;
            }

            DecreaseAttempt();

            if (sum == _point)
            {
                FinishRound(image, point ,isPlayerTurn, true);
                return;
            }

            if (sum == 7)
            {
                FinishRound(image, point, isPlayerTurn, false);
            }
        }
        
        
        public bool AttemptsEnded()
        {
            return _retry == 0 || _retry <= 0;
        }
        
        public void ChangeTurn(ITurnStateService newTurn)
        {
            _currentTurn = newTurn;
            _retry = 5;
            _retryLabel.text = _retry.ToString();
            _point = null;

            if (newTurn is CpuController)
            {
                TurnController._canRoll = false;
                RollCpu();
            }

            TurnController._canRoll = true;
        }
        
        public void RollCpu()
        {
            dice.CpuPlay();
        }
        
        private void FinishRound(Image image, Label point, bool isPlayerTurn, bool playerWonLogic)
        {
            
            var finalResult = isPlayerTurn switch
            {
                true  => playerWonLogic,
                false => !playerWonLogic
            };

            image.sprite = finalResult ? winSprite : loseSprite;

            _scoreMemory.Add(finalResult ? 1 : 0);

            _point = null;
            point.text = "Point: ";
            _rounded++;

            FinalGameCheck();
        }
        
        private void DecreaseAttempt()
        {
            _retry--;
            _retryLabel.text = _retry.ToString();
        }
        
        private void FinalGameCheck()
        {
            var win = _scoreMemory.Count(x => x == 1) >= 3;
            var lose = _scoreMemory.Count(x => x == 0) >= 3;

            if (win || lose)
            {
                TurnController._canRoll = false;
                Time.timeScale = 0f;
                Debug.Log(win ? "YOU WIN" : "YOU LOSE");
            }
        }
        

    }
}