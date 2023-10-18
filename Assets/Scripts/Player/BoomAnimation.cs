using System;
using System.Collections;
using System.Collections.Generic;
using Game.Controller;
using UnityEngine;

public class BoomAnimation : MonoBehaviour
{
    [SerializeField]
    private GameController gameController;
    [SerializeField]
    private GameObject boom;

    private void Awake()
    {
        gameController.GameOver += PlayAnimation;
    }

    private void OnDestroy()
    {
        gameController.GameOver -= PlayAnimation;
    }

    private void PlayAnimation()
    {
        boom.SetActive(true);
    }
}
