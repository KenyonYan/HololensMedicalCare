using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using UnityEngine.XR.WSA.Input;

public class ObjectController : MonoBehaviour, INavigationHandler, IManipulationHandler
{
    [Tooltip("Rotation max speed controls amount of rotation.")]
    [SerializeField]
    private float sliderSensitivity = 5.0f;
    private GestureRecognizer gestureRecognizer;
    private float eps = 0.00001f;
    private GameObject focusedObject;                      //注视的物体
    private Vector3 manipulationOriginalPosition = Vector3.zero;


    [Header("旋转敏感度")]
    private float RotationSensitivity = 10.0f;
    [Header("最大缩放尺寸")]
    public float maxSize = 0.2f;
    [Header("最小缩放尺寸")]
    public float minSize = 0.1f;

    [HideInInspector]
    [Header("移动模式")]
    public bool isMove = true;          
    [HideInInspector]
    [Header("旋转模式")]
    public bool isRotate = false;
    [HideInInspector]
    [Header("缩放模式")]
    public bool isZoom = false;



    // Use this for initialization
    void Start ()
    {
        //  创建GestureRecognizer实例
        gestureRecognizer = new GestureRecognizer();
        //  注册指定的手势类型,本例指定双击手势类型
        gestureRecognizer.SetRecognizableGestures(GestureSettings.Tap | GestureSettings.DoubleTap | GestureSettings.Hold);
        //  订阅手势事件
        gestureRecognizer.TappedEvent += GestureRecognizer_TappedEvent;
        //  开始手势识别
        gestureRecognizer.StartCapturingGestures();
    }

    void Update()
    {
        var headPosition = Camera.main.transform.position;
        var gazeDirection = Camera.main.transform.forward;

        RaycastHit hitInfo;

        if (Physics.Raycast(headPosition, gazeDirection, out hitInfo))
        {
            focusedObject = hitInfo.collider.gameObject;
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            OnDoubleTap();
        }
    }

    private void GestureRecognizer_TappedEvent(InteractionSourceKind source, int tapCount, Ray headRay)
    {
        if (tapCount == 1)
        {
            OnSingleTap();         //单击事件触发移动功能
        }
        else if (tapCount == 2)
        {
            OnDoubleTap();          //双击事件触发旋转功能
        }
        else if(tapCount == 4)
        {
            OnHoldTap();            //长按触发缩放功能
        }
    }

    private void OnSingleTap()
    {
        if (focusedObject.name == this.gameObject.name)           //视线碰撞物体为当前物体时
        {
            isMove = true;
            isRotate = false;
            isZoom = false;
        }
    }


    private void OnDoubleTap()
    {
        if (focusedObject.name == this.gameObject.name)           //视线碰撞物体为当前物体时
        {
            isMove = false;
            isRotate = true;
            isZoom = false;
        }
    }

    private void OnHoldTap()
    {
        if (focusedObject.name == this.gameObject.name)           //视线碰撞物体为当前物体时
        {
            isMove = false;
            isRotate = false;
            isZoom = true;
        }
    }

    void INavigationHandler.OnNavigationStarted(NavigationEventData eventData)
    {
        InputManager.Instance.PushModalInputHandler(gameObject);
    }

    void INavigationHandler.OnNavigationUpdated(NavigationEventData eventData)
    {
        if (isMove)
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

        if (isZoom)
        {
            float sliderOffset = eventData.NormalizedOffset.x * sliderSensitivity;
            if (transform.localScale.x - maxSize >= eps)
            {
                transform.localScale = new Vector3(maxSize, maxSize, maxSize);
            }
            else if (minSize - transform.localScale.x >= eps)
            {
                transform.localScale = new Vector3(minSize, minSize, minSize);
            }
            else
            {
                float NewX = transform.localScale.x + sliderOffset;
                float NewY = transform.localScale.y + sliderOffset;
                float NewZ = transform.localScale.z + sliderOffset;
                transform.localScale = new Vector3(NewX, NewY, NewZ);
            }
        }
        
    }

    void INavigationHandler.OnNavigationCompleted(NavigationEventData eventData)
    {

    }

    void INavigationHandler.OnNavigationCanceled(NavigationEventData eventData)
    {
        InputManager.Instance.PopModalInputHandler();
    }

    void IManipulationHandler.OnManipulationStarted(ManipulationEventData eventData)
    {
        if (isRotate)
        {
            InputManager.Instance.PushModalInputHandler(gameObject);

            manipulationOriginalPosition = transform.position;
        }
    }

    void IManipulationHandler.OnManipulationUpdated(ManipulationEventData eventData)
    {
        if (isRotate)
        {
            /* TODO: DEVELOPER CODING EXERCISE 4.a */

            // 4.a: Make this transform's position be the manipulationOriginalPosition + eventData.CumulativeDelta
            transform.position = manipulationOriginalPosition + eventData.CumulativeDelta;
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
