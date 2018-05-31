using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerControlMin : MonoBehaviour 
{
    [SerializeField]
    private MinsPool minsPool;

    [SerializeField]
    private MinsType currentMinType;

    private int numTypes;
    private Dictionary<MinsType, Stack<MinLight>> controlledMins;

    private void Awake()
    {
        currentMinType = MinsType.Block;
        var minTypes = Enum.GetValues(typeof(MinsType)).Cast<MinsType>().ToArray();
        numTypes = minTypes.Length - 1;
        controlledMins = new Dictionary<MinsType, Stack<MinLight>>();

        InitializeControlledMins(minTypes);
    }

    private void Update()
    {
        CycleCurrentMinTypeInput();
        MinTypeActions();
    }

    private void CycleCurrentMinType()
    {
        var currentMinInt = (int)currentMinType;
        currentMinInt++;
        currentMinInt %= numTypes;
        currentMinType = (MinsType)currentMinInt;
    }

    private void CycleCurrentMinTypeInput()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            CycleCurrentMinType();
        }
    }

    private void InitializeControlledMins(MinsType [] minTypes)
    {
        foreach(var minType in minTypes)
        {
            if(!controlledMins.ContainsKey(minType))
            {
                controlledMins.Add(minType, new Stack<MinLight>());
            }
        }
    }

    public void AddMinToControlledMins(MinLight min)
    {
        controlledMins[min.BaseMin.MinType].Push(min);
    }

    private void MinTypeActions()
    {

    }

    //Add a way to remove Mins from the controlled mins. Cases where the min gets destroyed.
}
