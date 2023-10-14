using System;
using UnityEngine;


namespace Game.Controller
{
    public class GameController : MonoBehaviour
    {
        private const string ENEMY = "Enemy";

        public Action GameOver;
    
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag(ENEMY))
            {
                GameOver?.Invoke();
            }
        }
    }

}
