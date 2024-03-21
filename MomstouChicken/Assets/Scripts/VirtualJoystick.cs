using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private RectTransform lever;
    private RectTransform rectTransform;

    [SerializeField, Range(10, 150)]
    private float leverRange;

    private Vector2 inputDirection;
    private bool isInput;

    [SerializeField]
    private PlayerController playerController;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void FixedUpdate()
    {
        InputControlVector();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        ControlJoystickLever(eventData);
        isInput = true;
    }

    // 오브젝트를 클릭해서 드래그 하는 도중에 들어오는 이벤트
    // 클릭을 유지한 상태로 마우스를 멈추면 이벤트가 들어오지 않음
    public void OnDrag(PointerEventData eventData)
    {
        ControlJoystickLever(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // 터치를 떼면 원위치로 돌아오도록
        lever.anchoredPosition = Vector2.zero;
        isInput = false;
        playerController.Move(Vector2.zero);
    }

    private void ControlJoystickLever(PointerEventData eventData)
    {
        // 레버가 있어야 할 위치 = 터치한 위치값 - 가상 조이스틱의 위치
        var inputPos = eventData.position - rectTransform.anchoredPosition;
        Vector2 inputVector;
        if (inputPos.magnitude < leverRange)
        {
            inputVector = inputPos;
        }
        else
        {
            inputVector = inputPos.normalized * leverRange;
        }
        lever.anchoredPosition = inputVector;
        inputDirection = inputVector / leverRange; // inputVector는 해상도를 기반으로 한 값이라 캐릭터의 이동속도로 쓰기엔 값이 너무 큼.
    }

    // 캐릭터에게 입력벡터를 전달
    private void InputControlVector()
    {
        if (isInput)
        {
            playerController.Move(inputDirection);
        }
    }
}
