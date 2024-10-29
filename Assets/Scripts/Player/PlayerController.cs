using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [Header("Movement")]
    public float moveSpeed;
    public float jumpPower;
    private Vector2 curMovementInput; // curMovementInput���� Ű�Է��� �� ������ ���ο� ���� ���� �ȴ�.
    private Rigidbody _rigidbody;
    public LayerMask groundLayerMask;


    [Header("Look")]
    public Transform cameraContainer;
    public float minXLook; //�ּ� ȸ������
    public float maxXLook; //�ִ� ȸ������
    private float camCurXRot; //���콺�� ��Ÿ���� �޾ƿ� ����
    public float lookSensitivity; //ȸ�� �ΰ���
    private Vector2 mouseDelta;

    public bool canLook = true;
    public Action Inventory;

    bool isFunctionActive = false;
    float startTime;

    float originalSpeed;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        originalSpeed = moveSpeed;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //���콺 Ŀ�� ������ �ʰ� ��
    }

    private void Update()
    {
        // ����� Ȱ��ȭ�Ǿ� �ְ� ���� �ð��� ����ϸ� ����� ��Ȱ��ȭ�մϴ�.
        if (isFunctionActive && Time.time - startTime >= 5f)
        {
            StopSpeedUp();
        }
    }

    void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate() //ī�޶� ���ư��� �� ����
    {
        if (canLook)
        {
            CameraLook();
        }
    }

    public void StartSpeedUp(float speedMultiple)
    {
        // ����� Ȱ��ȭ�ϰ� ���� �ð��� ����մϴ�.
        isFunctionActive = true;
        moveSpeed *= speedMultiple;
        startTime = Time.time;
    }

    public void StopSpeedUp()
    {
        // ����� ��Ȱ��ȭ�մϴ�.
        moveSpeed = originalSpeed;
        isFunctionActive = false;
    }

    void Move()
    {
        Debug.Log("moveSpeed: " + moveSpeed);
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x; //���Ⱚ�� ���ϰ�
        dir *= moveSpeed; //���⿡ �ӷ�(speed)�� �����ش�.
        dir.y = _rigidbody.velocity.y; //���ν�Ƽ�� �ִ� y���� �ִ� ������, ������ �������� ���Ʒ��� ���������ϱ⿡, �̼��� ���� ������Ű�� ���� �ִ´�.

        _rigidbody.velocity = dir; //�׷��� ���õ� ���� velocity�� ����.
    }

    void CameraLook()
    {
        camCurXRot += mouseDelta.y * lookSensitivity; //���콺�� �¿�� �����̸� moushDelta.x�� �ٲ� ���Ϸ� �����̸� mouseDelta.y�� �ٲ�.
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook); //camCurXRot�� �ּҰ����� �۾����� �ּҰ��� ��ȯ, �ִ밪���� Ŀ���� �ִ��� ��ȯ�ϴ� �Լ�.
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }

    //Event�� ����� �� �ֵ��� ������� �Լ�
    //OnMove�� ������ָ� Context���� ���ؼ� ���� ����.
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed) //Ű�� �����ڿ��� ���� ��� ���� �� �־����
        {
            curMovementInput = context.ReadValue<Vector2>(); //���� ���� �о�´�.
        }
        else if (context.phase == InputActionPhase.Canceled) //Ű�� �������� ��
        {
            curMovementInput = Vector2.zero; //���� 0
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>(); //Vector2�� �о��.
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started & IsGrounded())
        {
            _rigidbody.AddForce(Vector2.up * jumpPower, ForceMode.Impulse);
        }
    }

    bool IsGrounded()
    {
        Ray[] rays = new Ray[4] //���� 4��, �ٸ� 4����� ���� ��.
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down)
        };

        for (int i = 0; i < rays.Length; i++)
        {
            //raycast�� ���� true,false ��ȯ, // 4���� ���̸� �� ��ȸ�� �غ���. 0.1���� ����. groundLayerMask�� �ش�Ǵ� �ֵ鸸 �����Ѵ�.
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask))
            {
                return true;
            }
        }
        return false;
    }

    public void OnInventory(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        {
            Inventory?.Invoke();
            ToggleCursor();
        }
    }

    //cursor�� toggle���ִ� ���.
    void ToggleCursor()
    {
        bool toggle = Cursor.lockState == CursorLockMode.Locked; //���� �ɷ��ִٸ�
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked; //Ŀ�� �����¸� ����� Ʈ���� none, false��� ���� �Ǵ�.
        canLook = !toggle; //��۰��� �ݴ�� ����.
    }
}
