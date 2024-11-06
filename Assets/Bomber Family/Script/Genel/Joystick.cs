using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private RectTransform container;
    private RectTransform handle;

    /// <summary>
    /// Klavyede işliyorsa
    /// </summary>
    public bool ArrowKeysSimulationEnabled = false;

    private Vector2 point;
    private Vector2 normalizedPoint;
    private float maxLength;
    private bool isTouching = false;
    private PointerEventData pointerEventData;
    private Camera cam;

    public bool IsTouching { get { return isTouching; } }

    public UnityAction OnJoystickDownAction;
    public UnityAction OnJoystickUpAction;

    private void OnEnable()
    {
        OnPointerUp(null);
    }
    private void Awake()
    {
        container = transform.GetComponent<RectTransform>();
        handle = container.GetChild(0).GetComponent<RectTransform>();

        maxLength = (container.sizeDelta.x / 2f) - (handle.sizeDelta.x / 2f) - 5f;
    }
    public void OnPointerDown(PointerEventData e)
    {
        if (OnJoystickDownAction != null)
            OnJoystickDownAction.Invoke();

        isTouching = true;
        cam = e.pressEventCamera;
        OnDrag(e);
    }
    public void OnDrag(PointerEventData e)
    {
        pointerEventData = e;
    }
    void Update()
    {
        if (isTouching && RectTransformUtility.ScreenPointToLocalPointInRectangle(container, pointerEventData.position, cam, out point))
        {
            point = Vector2.ClampMagnitude(point, maxLength);
            handle.anchoredPosition = point;

            float length = Mathf.InverseLerp(0f, maxLength, point.magnitude);
            normalizedPoint = Vector2.ClampMagnitude(point, length);
        }
    }
    public void OnPointerUp(PointerEventData e)
    {
        if (OnJoystickUpAction != null)
            OnJoystickUpAction.Invoke();

        isTouching = false;
        normalizedPoint = Vector3.zero;
        handle.anchoredPosition = Vector3.zero;
    }
    /// <summary>
    /// -1 ve 1 arasında yatay hareket
    /// </summary>
    public float MoveHorizontal()
    {
        if (ArrowKeysSimulationEnabled)
            return (normalizedPoint.x != 0) ? normalizedPoint.x : Input.GetAxisRaw("Horizontal");

        return normalizedPoint.x;
    }
    /// <summary>
    /// -1 ve 1 arasında dikey hareket
    /// </summary>
    public float MoveVertical()
    {
        if (ArrowKeysSimulationEnabled)
            return (normalizedPoint.y != 0) ? normalizedPoint.y : Input.GetAxisRaw("Vertical");

        return normalizedPoint.y;
    }
    public Vector3 Direction()
    {
        return new Vector3(MoveHorizontal(), 0, MoveVertical()).normalized;
    }
}