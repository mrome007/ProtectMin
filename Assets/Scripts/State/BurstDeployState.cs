using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstDeployState : IDeployState 
{
    public IDeployState NextState { get; private set; }

    private int burstGroupNumber = 4;

    public BurstDeployState()
    {
        NextState = null;
    }

    public void SetNextState(IDeployState next)
    {
        NextState = next;
    }

    public void HandleInput(Vector3 currentPos, ref Dictionary<MinsType, Stack<MinLight>> mins)
    {
        if(Input.GetMouseButtonDown(0))
        {
            var worldPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, currentPos.z - Camera.main.transform.position.z));

            var numDeployed = 0;
            while(mins[MinsType.Burst].Count > 0 && numDeployed < burstGroupNumber)
            {
                var minLight = mins[MinsType.Burst].Pop();

                var movement = minLight.GetComponent<MinMovement>();
                (movement as BurstMinMovement).SetBurstTarget(worldPos);
                movement.SetMovementType(MinMovement.MinMovementType.Deploy);

                numDeployed++;
            }
        }
    }
}
