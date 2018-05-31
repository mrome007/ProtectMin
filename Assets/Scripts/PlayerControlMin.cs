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

    #region Deploy State

    private IDeployState currentDeployState;
    private BlockDeployState blockDeployState;
    private FloorDeployState floorDeployState;
    private JumpDeployState jumpDeployState;
    private ProjectileDeployState projectileDeployState;
    private bool deployStateInitialized = false;

    #endregion

    private void Awake()
    {
        currentMinType = MinsType.Block;
        var minTypes = Enum.GetValues(typeof(MinsType)).Cast<MinsType>().ToArray();
        numTypes = minTypes.Length - 1;
        controlledMins = new Dictionary<MinsType, Stack<MinLight>>();

        InitializeDeployStates();
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
        currentDeployState = currentDeployState.NextState;
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

    private void InitializeDeployStates()
    {
        if(!deployStateInitialized)
        {
            blockDeployState = new BlockDeployState();
            floorDeployState = new FloorDeployState();
            jumpDeployState = new JumpDeployState();
            projectileDeployState = new ProjectileDeployState();

            blockDeployState.SetNextState(floorDeployState);
            floorDeployState.SetNextState(jumpDeployState);
            jumpDeployState.SetNextState(projectileDeployState);
            projectileDeployState.SetNextState(blockDeployState);

            currentDeployState = blockDeployState;

            deployStateInitialized = true;
        }
    }

    public void AddMinToControlledMins(MinLight min)
    {
        controlledMins[min.BaseMin.MinType].Push(min);
    }

    private void MinTypeActions()
    {
        if(currentDeployState != null)
        {
            currentDeployState.HandleInput(transform.position, ref controlledMins);
        }
    }


}
