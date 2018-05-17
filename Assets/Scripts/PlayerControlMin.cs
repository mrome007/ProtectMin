using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlMin : MonoBehaviour 
{
    [SerializeField]
    private MinsPool MinsPool;

    [SerializeField]
    private MinsType CurrentMinType;

    private int numTypes;
    private Dictionary<string, List<MinLight>> controlledMins;

    private void Awake()
    {
        CurrentMinType = MinsType.Block;
        var minTypes = Enum.GetNames(typeof(MinsType));
        numTypes = minTypes.Length;
        controlledMins = new Dictionary<string, List<MinLight>>();

        InitializeControlledMins(minTypes);
    }

    private void Update()
    {
        CycleCurrentMinTypeInput();
    }

    private void CycleCurrentMinType()
    {
        var currentMinInt = (int)CurrentMinType;
        currentMinInt++;
        currentMinInt %= numTypes;
        CurrentMinType = (MinsType)currentMinInt;
    }

    private void CycleCurrentMinTypeInput()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            CycleCurrentMinType();
        }
    }

    private void InitializeControlledMins(string [] minTypes)
    {
        foreach(var minType in minTypes)
        {
            if(!controlledMins.ContainsKey(minType))
            {
                controlledMins.Add(minType, new List<MinLight>());
            }
        }
    }

    public void AddMinToControlledMins(MinLight min)
    {
        controlledMins[min.BaseMin.MinType.ToString()].Add(min);
    }

    //Add a way to remove Mins from the controlled mins. Cases where the min gets destroyed.
}
