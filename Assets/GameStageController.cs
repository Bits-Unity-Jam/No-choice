using Game.Energy;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStageController : MonoBehaviour
{
    [SerializeField]
    private PointerCatcher _pointerCatcher;

    [SerializeField]
    private bool _isGameStarted;

    [SerializeField]
    private EnergyController energyController;

    [SerializeField]
    private ConstantMove constantMove;

    [SerializeField]
    private List<GameObject> objectsToHideInMenu;
    [SerializeField]
    private List<GameObject> objectsToHideInGame;

    private void Awake()
    {
        _pointerCatcher.OnPointerDownCaught += HandlePointerDownCaught;
        constantMove.Speed = 0;
        objectsToHideInMenu.ForEach(obj => obj.SetActive(false));
    }

    private void HandlePointerDownCaught()
    {
        if (_isGameStarted) { return; }

        objectsToHideInMenu.ForEach(obj => obj.SetActive(true));
        objectsToHideInGame.ForEach(obj => obj.SetActive(false));
        constantMove.Speed = constantMove.DefaultSpeed;
        _isGameStarted = true;
        energyController.StartTimer();
    }
}
