using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpDeployState : IDeployState 
{
    public IDeployState NextState { get; private set; }

    public JumpDeployState()
    {
        NextState = null;
    }

    public void SetNextState(IDeployState next)
    {
        NextState = next;
    }

    public void HandleInput(Vector3 currentPos, ref Dictionary<MinsType, Stack<MinLight>> mins)
    {

    }
}
