using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour 
{
    [SerializeField]
    private Transform FirstBackground;

    [SerializeField]
    private Transform SecondBackground;

    [SerializeField]
    private float outOfCameraLimit;

    [SerializeField]
    private Transform resetTransform;

    [SerializeField]
    private float scrollSpeed;

    private void Update()
    {
        FirstBackground.transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime);
        SecondBackground.transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime);

        if(FirstBackground.transform.localPosition.x <= outOfCameraLimit)
        {
            FirstBackground.transform.localPosition = resetTransform.localPosition;
        }

        if(SecondBackground.transform.localPosition.x <= outOfCameraLimit)
        {
            SecondBackground.transform.localPosition = resetTransform.localPosition;
        }
    }
}
