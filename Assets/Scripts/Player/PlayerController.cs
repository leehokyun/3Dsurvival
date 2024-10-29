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
    private Vector2 curMovementInput; // curMovementInput에는 키입력이 들어갈 때마다 새로운 값이 들어가게 된다.
    private Rigidbody _rigidbody;
    public LayerMask groundLayerMask;


    [Header("Look")]
    public Transform cameraContainer;
    public float minXLook; //최소 회전범위
    public float maxXLook; //최대 회전범위
    private float camCurXRot; //마우스의 델타값을 받아온 변수
    public float lookSensitivity; //회전 민감도
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
        Cursor.lockState = CursorLockMode.Locked; //마우스 커서 보이지 않게 함
    }

    private void Update()
    {
        // 기능이 활성화되어 있고 일정 시간이 경과하면 기능을 비활성화합니다.
        if (isFunctionActive && Time.time - startTime >= 5f)
        {
            StopSpeedUp();
        }
    }

    void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate() //카메라가 돌아가는 것 조정
    {
        if (canLook)
        {
            CameraLook();
        }
    }

    public void StartSpeedUp(float speedMultiple)
    {
        // 기능을 활성화하고 시작 시간을 기록합니다.
        isFunctionActive = true;
        moveSpeed *= speedMultiple;
        startTime = Time.time;
    }

    public void StopSpeedUp()
    {
        // 기능을 비활성화합니다.
        moveSpeed = originalSpeed;
        isFunctionActive = false;
    }

    void Move()
    {
        Debug.Log("moveSpeed: " + moveSpeed);
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x; //방향값을 정하고
        dir *= moveSpeed; //방향에 속력(speed)을 곱해준다.
        dir.y = _rigidbody.velocity.y; //벨로시티에 있는 y값을 넣는 이유는, 점프를 했을때만 위아래로 움직여야하기에, 미세한 값을 유지시키기 위해 넣는다.

        _rigidbody.velocity = dir; //그렇게 세팅된 값을 velocity에 넣음.
    }

    void CameraLook()
    {
        camCurXRot += mouseDelta.y * lookSensitivity; //마우스를 좌우로 움직이면 moushDelta.x가 바뀜 상하로 움직이면 mouseDelta.y가 바뀜.
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook); //camCurXRot가 최소값보다 작아지면 최소값을 반환, 최대값보다 커지면 최댓값을 반환하는 함수.
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }

    //Event를 등록할 수 있도록 등록해줄 함수
    //OnMove를 등록해주면 Context값을 통해서 값을 전달.
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed) //키가 눌린뒤에도 값을 계속 받을 수 있어야함
        {
            curMovementInput = context.ReadValue<Vector2>(); //벡터 값을 읽어온다.
        }
        else if (context.phase == InputActionPhase.Canceled) //키가 떨어졌을 때
        {
            curMovementInput = Vector2.zero; //값은 0
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>(); //Vector2값 읽어옴.
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
        Ray[] rays = new Ray[4] //레이 4개, 다리 4개라고 보면 됨.
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down)
        };

        for (int i = 0; i < rays.Length; i++)
        {
            //raycast를 통해 true,false 반환, // 4개의 레이를 쭉 순회를 해본다. 0.1정도 길이. groundLayerMask에 해당되는 애들만 검출한다.
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

    //cursor를 toggle해주는 기능.
    void ToggleCursor()
    {
        bool toggle = Cursor.lockState == CursorLockMode.Locked; //락이 걸려있다면
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked; //커서 락상태를 토글이 트루라면 none, false라면 락을 건다.
        canLook = !toggle; //토글값과 반대로 세팅.
    }
}
