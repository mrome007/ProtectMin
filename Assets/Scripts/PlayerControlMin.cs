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
    private float blockDeployOffset;

    private MinsType currentMinType;
    private int numTypes;
    private Dictionary<MinsType, Stack<MinLight>> controlledMins;
    private int deploySpriteOrder = 0;
    private Vector3? previousPos;

    private void Awake()
    {
        currentMinType = MinsType.Block;
        var minTypes = Enum.GetValues(typeof(MinsType)).Cast<MinsType>().ToArray();
        numTypes = minTypes.Length;
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
        switch(currentMinType)
        {
            case MinsType.Block:
                DeployMinsTypeBlock();
                break;

            case MinsType.Floor:
                break;

            case MinsType.Jump:
                break;

            case MinsType.Projectile:
                break;

            default:
                break;
        }
    }

    private void DeployMinsTypeBlock()
    {
        if(Input.GetMouseButton(0))
        {
            var worldPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z - Camera.main.transform.position.z));

            var deploy = !previousPos.HasValue;
            if(previousPos.HasValue)
            {
                deploy = (Mathf.Abs((worldPos.x - previousPos.Value.x)) >= blockDeployOffset) || (Mathf.Abs((worldPos.y - previousPos.Value.y)) >= blockDeployOffset);
            }
                
            if(deploy && controlledMins[MinsType.Block].Count > 0)
            {
                var minLight = controlledMins[MinsType.Block].Pop();
                minLight.GetComponent<MinFlockMovement>().enabled = false;
                minLight.transform.position = worldPos;
                minLight.GetComponent<DeployedMovement>().enabled = true;
                minLight.UpdateMinLightSpriteOrder(deploySpriteOrder++);
            }

            previousPos = worldPos;
        }

        if(Input.GetMouseButtonUp(0))
        {
            deploySpriteOrder = 0;
        }
    }

    //Add a way to remove Mins from the controlled mins. Cases where the min gets destroyed.
}
