using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDeployState
{
    IDeployState NextState { get; }
    void SetNextState(IDeployState next);
    void HandleInput(Vector3 currentPos, ref Dictionary<MinsType, Stack<MinLight>> mins);
}
