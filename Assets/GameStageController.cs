using Game.Energy;
using System;
using System.Collections;
using System.Collections.Generic;
using Effects.Parallax;
using Game.UI.Popups;
using UnityEngine;

public class GameStageController : MonoBehaviour
{
    [SerializeField]
    private PointerCatcher _pointerCatcher;

    [SerializeField]
    private bool _isGameStarted;

    [SerializeField]
    private EnergyController energyController;

    [SerializeField] private ScreenElementLocomotionService _locomotionService;

    [SerializeField]
    private List<BaseActivatingTween>tweensToHideInMenu;
    [SerializeField]
    private List<GameObject> objectsToHideInMenu;
    [SerializeField]
    private List<GameObject> objectsToHideInGame;
    [SerializeField]
    private CanvasElementSequenceActivatingTween  _elementSequenceActivatingTween;

    private void Awake()
    {  
        _elementSequenceActivatingTween.DoDeactivateImmediately();
        _pointerCatcher.OnPointerDownCaught += HandlePointerDownCaught;
        tweensToHideInMenu.ForEach(obj => obj.DoActivate());
        objectsToHideInMenu.ForEach(obj => obj.SetActive(false));
    }

    private void Start()
    {
        _locomotionService.GeneralMotionSpeed = 0;
        _elementSequenceActivatingTween.DoActivate();
    }

    private void HandlePointerDownCaught()
    {
        if (_isGameStarted) { return; }

        _locomotionService.GeneralMotionSpeed = 7;
        _elementSequenceActivatingTween.DoDeactivate();
        tweensToHideInMenu.ForEach(obj => obj.DoDeactivate());
        objectsToHideInMenu.ForEach(obj => obj.SetActive(true));
        objectsToHideInGame.ForEach(obj => obj.SetActive(false));
        _isGameStarted = true;
        energyController.StartTimer();
    }
}
