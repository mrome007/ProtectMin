using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorDeployState : IDeployState
{
    public IDeployState NextState { get; private set; }

    public FloorDeployState(IDeployState next)
    {
        NextState = next;
    }

    public void HandleInput(Vector3 currentPos, ref Dictionary<MinsType, Stack<MinLight>> mins)
    {
        if(Input.GetMouseButtonDown(0))
        {
            var worldPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, currentPos.z - Camera.main.transform.position.z));
            if(mins[MinsType.Floor].Count > 0)
            {
                while(mins[MinsType.Floor].Count > 0)
                {
                    var minLight = mins[MinsType.Floor].Pop();

                    var movement = minLight.GetComponent<MinMovement>();
                    (movement as FloorMinMovement).SetFloorTarget(worldPos);
                    movement.SetMovementType(MinMovement.MinMovementType.Deploy);
                }
            }
        }
    }
}
