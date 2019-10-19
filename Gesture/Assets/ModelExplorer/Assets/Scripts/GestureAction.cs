using HoloToolkit.Unity.InputModule;
using UnityEngine;



public enum GestureType
{
    Move,
    Rotate,
    Zoom
}

/// <summary>
/// GestureAction performs custom actions based on
/// which gesture is being performed.
/// </summary>
/// 
public class GestureAction : MonoBehaviour, INavigationHandler, IManipulationHandler, ISpeechHandler
{
    [Header("手势类型")]
    public GestureType gestureType;

    [Header("最大缩放尺寸")]
    public float maxSize = 0.2f;
    [Header("最小缩放尺寸")]
    public float minSize = 0.1f;
    [Header("缩放偏移精度")]
    private float eps = 0.00001f;


    [Tooltip("Rotation max speed controls amount of rotation.")]
    [SerializeField]
    [Header("旋转敏感度")]
    private float RotationSensitivity = 10.0f;
    private bool isNavigationEnabled = true;              //移动(false)和旋转(true)之间的切换


    void Start()
    {
        gestureType = GestureType.Rotate;                 //开始默认为旋转模式
    }


    public bool IsNavigationEnabled
    {
        get { return isNavigationEnabled; }
        set { isNavigationEnabled = value; }
    }

    public GestureType GestureControl
    {
        get { return gestureType; }
        set { gestureType = value; }
    }

    private Vector3 manipulationOriginalPosition = Vector3.zero;

    void INavigationHandler.OnNavigationStarted(NavigationEventData eventData)
    {
        InputManager.Instance.PushModalInputHandler(gameObject);
    }

    void INavigationHandler.OnNavigationUpdated(NavigationEventData eventData)
    {
        if (gestureType == GestureType.Zoom)
        {
            float sliderOffset = eventData.NormalizedOffset.x * RotationSensitivity;
            print(sliderOffset);
            GameObject model = GameObject.FindGameObjectWithTag("Model");

            if (model.transform.localScale.x - maxSize >= eps)
            {
                model.transform.localScale = new Vector3(maxSize, maxSize, maxSize);
            }
            else if (minSize - model.transform.localScale.x >= eps)
            {
                model.transform.localScale = new Vector3(minSize, minSize, minSize);
            }
            else
            {
                float NewX = model.transform.localScale.x + sliderOffset;
                float NewY = model.transform.localScale.y + sliderOffset;
                float NewZ = model.transform.localScale.z + sliderOffset;
                model.transform.localScale = new Vector3(NewX, NewY, NewZ);
            }
        }
        if (gestureType == GestureType.Rotate)
        {
            float rotationFactor = eventData.NormalizedOffset.x * RotationSensitivity;
            transform.Rotate(new Vector3(0, -1 * rotationFactor, 0));
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
        if (gestureType == GestureType.Move)
        {
            InputManager.Instance.PushModalInputHandler(gameObject);

            manipulationOriginalPosition = transform.position;
        }
    }

    void IManipulationHandler.OnManipulationUpdated(ManipulationEventData eventData)
    {
        if (gestureType == GestureType.Move)
        {
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

    void ISpeechHandler.OnSpeechKeywordRecognized(SpeechEventData eventData)
    {
        if (eventData.RecognizedText.Equals("Move"))
        {
            isNavigationEnabled = false;
            gestureType = GestureType.Move;
        }
        else if (eventData.RecognizedText.Equals("Rotate"))
        {
            isNavigationEnabled = true;
            gestureType = GestureType.Rotate;
        }
        else
        {
            return;
        }
        eventData.Use();
    }
}