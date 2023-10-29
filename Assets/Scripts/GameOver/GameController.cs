using System;
using UnityEngine;


namespace Game.Controller
{
    public class GameController : MonoBehaviour
    {
        [Header("Boom Objects")]
        [SerializeField]
        private AudioSource boomSource;
        [SerializeField]
        private AudioSource audioSource;
        [SerializeField]
        private GameObject boom;
        
        private const string ENEMY = "Enemy";

        public Action GameOver;
        private bool _gameIsOver = false;
    
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag(ENEMY))
            {
                if (!_gameIsOver)
                {
                    _gameIsOver = true;
                    
                    audioSource.Play();
                    boomSource.Play();
                    
                    boom.SetActive(true);
                    GameOver?.Invoke();
                }
                    
            }
        }
    }

}
