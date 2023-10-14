using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
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
        
        private int _lastScore;
        private int _bestScore;

        private Vector3 _startPoint;
        private Vector3 _currentPoint;

        private float _currentDistance;
        
        private const string BEST_SCORE = "BestScore";
        private const string LAST_SCORE = "LastScore";

        private void Awake()
        {
            _startPoint = player.position;
            
            SetBestScore();
        }

        private void Update()
        {
            _currentPoint = player.position;
            _currentDistance = Vector3.Distance(_startPoint, _currentPoint);

            textScore.text = ((int)_currentDistance).ToString();
        }

        private void SaveBestScore()
        {
            if (_bestScore < _lastScore)
            {
                _bestScore = _lastScore;
                PlayerPrefs.SetFloat(BEST_SCORE, _bestScore);
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
            }
            
            bestScore.text = (_bestScore).ToString();
        }
        
    }
}
