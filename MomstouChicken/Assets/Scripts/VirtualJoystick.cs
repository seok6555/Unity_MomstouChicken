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

    // ������Ʈ�� Ŭ���ؼ� �巡�� �ϴ� ���߿� ������ �̺�Ʈ
    // Ŭ���� ������ ���·� ���콺�� ���߸� �̺�Ʈ�� ������ ����
    public void OnDrag(PointerEventData eventData)
    {
        ControlJoystickLever(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // ��ġ�� ���� ����ġ�� ���ƿ�����
        lever.anchoredPosition = Vector2.zero;
        isInput = false;
        playerController.Move(Vector2.zero);
    }

    private void ControlJoystickLever(PointerEventData eventData)
    {
        // ������ �־�� �� ��ġ = ��ġ�� ��ġ�� - ���� ���̽�ƽ�� ��ġ
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
        inputDirection = inputVector / leverRange; // inputVector�� �ػ󵵸� ������� �� ���̶� ĳ������ �̵��ӵ��� ���⿣ ���� �ʹ� ŭ.
    }

    // ĳ���Ϳ��� �Էº��͸� ����
    private void InputControlVector()
    {
        if (isInput)
        {
            playerController.Move(inputDirection);
        }
    }
}
