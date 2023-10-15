using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Game.Controller;
using TMPro;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Game.Score
{
    public class Score : MonoBehaviour
    {
        [Header("Text")]
        [SerializeField]
        private TMP_Text textScore;
        [SerializeField]
        private TMP_Text bestScore;

        [Header("Player")]
        [SerializeField]
        private Transform player;
        [SerializeField]
        private GameController gameController;
        
        private int _lastScore;
        private int _bestScore;

        private Vector3 _startPoint;
        private Vector3 _currentPoint;

        private float _currentDistance;
        
        private const string BEST_SCORE = "BestScore";

        private bool _stopScore = false;

        private void Awake()
        {
            _startPoint = player.position;
            
            SetBestScore();

            gameController.GameOver += SaveBestScore;
        }

        private void Start()
        {
            SetBestScore();
        }

        private void OnDestroy()
        {
            gameController.GameOver -= SaveBestScore;
        }

        private void Update()
        {
            if(!_stopScore)
            {
                _currentPoint = player.position;
                _currentDistance = Vector3.Distance(_startPoint, _currentPoint);

                textScore.text = ((int)_currentDistance).ToString();
            }
        }

        private void SaveBestScore()
        {
            _lastScore = (int)_currentDistance;
            _stopScore = true;
            
            if (_bestScore < _lastScore)
            {
                _bestScore = _lastScore;
                PlayerPrefs.SetInt(BEST_SCORE, _bestScore);
            }
        }
        
        private void SetBestScore()
        {
            if (PlayerPrefs.HasKey(BEST_SCORE)) 
            {
                _bestScore = PlayerPrefs.GetInt(BEST_SCORE);
            } 
            else 
            {
                _bestScore = 0;
                PlayerPrefs.SetFloat(BEST_SCORE, _bestScore);
            }
            
            bestScore.text = (_bestScore).ToString();
        }
        
    }
}
