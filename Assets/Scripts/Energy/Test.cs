using System.Collections;
using System.Collections.Generic;
using Game.Energy;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void Start()
    {
        EnergyController.Instance.StartTimer();
    }
    
    public void AddEnergy()
    {
        EnergyController.Instance.ChangeEnergy(10, EnergyOperation.Add);
    }
}
