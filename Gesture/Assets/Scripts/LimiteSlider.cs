﻿using HoloToolkit.Unity.InputModule;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// GestureAction performs custom actions based on
/// which gesture is being performed.
/// </summary>
public class LimiteSlider : MonoBehaviour, INavigationHandler, IManipulationHandler
{
    //public Slider slider;
    public GameObject sliderStartPoint;      //Slider滑动的开始点，解决Handle随物体移动保留在之前位置的问题


    private Vector3 startPostion;           //滑动条开始的位置
    private float sliderValue;              //Slider值   

    public float GetSliderValue()           //获取此时slider的值
    {
        return sliderValue;
    }

    void Start()
    {
        startPostion = this.transform.position;
    }


    [Tooltip("Rotation max speed controls amount of rotation.")]
    [SerializeField]
    private float RotationSensitivity = 10.0f;

    private bool isNavigationEnabled = true;
    public bool IsNavigationEnabled
    {
        get { return isNavigationEnabled; }
        set { isNavigationEnabled = value; }
    }

    private Vector3 manipulationOriginalPosition = Vector3.zero;

    void INavigationHandler.OnNavigationStarted(NavigationEventData eventData)
    {
        InputManager.Instance.PushModalInputHandler(gameObject);
    }

    void INavigationHandler.OnNavigationUpdated(NavigationEventData eventData)
    {
        if (isNavigationEnabled)
        {
            /* TODO: DEVELOPER CODING EXERCISE 2.c */

            // 2.c: Calculate a float rotationFactor based on eventData's NormalizedOffset.x multiplied by RotationSensitivity.
            // This will help control the amount of rotation.
            float rotationFactor = eventData.NormalizedOffset.x * RotationSensitivity;
            //float rotationFactorX = eventData.NormalizedOffset.y * RotationSensitivity;

            // 2.c: transform.Rotate around the Y axis using rotationFactor.
            transform.Rotate(new Vector3(0, -1 * rotationFactor, 0));
            //transform.Rotate(new Vector3(-1 * rotationFactorX, 0));
        }
    }

    void INavigationHandler.OnNavigationCompleted(NavigationEventData eventData)
    {
        InputManager.Instance.PopModalInputHandler();
    }

    void INavigationHandler.OnNavigationCanceled(NavigationEventData eventData)
    {
        InputManager.Instance.PopModalInputHandler();
    }

    void IManipulationHandler.OnManipulationStarted(ManipulationEventData eventData)
    {
        if (!isNavigationEnabled)
        {
            InputManager.Instance.PushModalInputHandler(gameObject);

            manipulationOriginalPosition = new Vector3(transform.position.x, startPostion.y, startPostion.z);
        }
    }

    void IManipulationHandler.OnManipulationUpdated(ManipulationEventData eventData)
    {
        if (!isNavigationEnabled)
        {
            /* TODO: DEVELOPER CODING EXERCISE 4.a */

            // 4.a: Make this transform's position be the manipulationOriginalPosition + eventData.CumulativeDelta
            //transform.position = manipulationOriginalPosition + eventData.CumulativeDelta;
            Vector3 tmp = manipulationOriginalPosition + new Vector3(eventData.CumulativeDelta.x, startPostion.y, startPostion.z);

            startPostion = sliderStartPoint.transform.position;
            if (tmp.x <= startPostion.x)
            {
                tmp = new Vector3(startPostion.x, startPostion.y, startPostion.z);
            }

            transform.position = new Vector3(tmp.x, startPostion.y, startPostion.z);
            //print(transform.position.x);
            //这里取得是世界坐标的值
            float terminalX = startPostion.x + 0.4197082f;
            if (transform.position.x >= terminalX)
            {
                transform.position = new Vector3(terminalX, startPostion.y, startPostion.z);
            }
            sliderValue = Mathf.Abs(transform.position.x - startPostion.x) / Mathf.Abs((startPostion.x + 0.4197082f) - startPostion.x);
        }
    }

    void IManipulationHandler.OnManipulationCompleted(ManipulationEventData eventData)
    {
        InputManager.Instance.PopModalInputHandler();
    }

    void IManipulationHandler.OnManipulationCanceled(ManipulationEventData eventData)
    {
        InputManager.Instance.PopModalInputHandler();
    }
}