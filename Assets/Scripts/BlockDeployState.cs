using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDeployState : IDeployState
{
    public IDeployState NextState { get; private set; }

    private float blockDeployOffset = 0.25f;
    private int deploySpriteOrder = 0;
    private Vector3? previousPos;

    public BlockDeployState()
    {
        NextState = null;
    }

    public void SetNextState(IDeployState next)
    {
        NextState = next;
    }
    
    public void HandleInput(Vector3 currentPos, ref Dictionary<MinsType, Stack<MinLight>> mins)
    {
        if(Input.GetMouseButton(0))
        {
            var worldPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, currentPos.z - Camera.main.transform.position.z));

            var deploy = !previousPos.HasValue;
            if(previousPos.HasValue)
            {
                deploy = (Mathf.Abs((worldPos.x - previousPos.Value.x)) >= blockDeployOffset) || (Mathf.Abs((worldPos.y - previousPos.Value.y)) >= blockDeployOffset);
            }

            if(deploy && mins[MinsType.Block].Count > 0)
            {
                var minLight = mins[MinsType.Block].Pop();

                var movement = minLight.GetComponent<MinMovement>();
                movement.SetMovementType(MinMovement.MinMovementType.Deploy);

                minLight.transform.position = worldPos;
                minLight.UpdateMinLightSpriteOrder(deploySpriteOrder++);
            }

            previousPos = worldPos;
        }

        if(Input.GetMouseButtonUp(0))
        {
            deploySpriteOrder = 0;
        }
    }
}
