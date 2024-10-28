using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{
    //카메라를 기준으로 RAY를 쏠때 얼마나 자주 업데이트해서 검출할지.

    public float checkRate = 0.05f;
    private float lastCheckTime;
    public float maxCheckDistance;
    public LayerMask layerMask; //어떤 레이어가 달려있는 게임오브젝트를 추출할건지?

    public GameObject curInteractGameObject; //인터렉션 성공했다면 그 게임오브젝트 정보를 갖기위해.
    private IInteractable curInteractable; //인터페이스 캐싱

    public TextMeshProUGUI promptText;
    private Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastCheckTime > checkRate)
        {
            lastCheckTime = Time.time;

            Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxCheckDistance, layerMask)) //ray정보 담아주고, 위에 있는 hit에다가 정보를 넘겨준다. 정보는 maxCheckDistance, layerMask정해주기
            {
                if (hit.collider.gameObject != curInteractGameObject) //기존에 현재 Interact하고있는 물체가 이미 존재할 수도 있는데, 그거랑 같지 않을떄!
                {
                    curInteractGameObject = hit.collider.gameObject;
                    curInteractable = hit.collider.GetComponent<IInteractable>();
                    SetPromptText();
                }
            }
            else //빈공간에 레이를 쐈을 경우
            {
                curInteractGameObject = null;
                curInteractable = null;
                promptText.gameObject.SetActive(false);
            }
        }
    }

    private void SetPromptText()
    {
        promptText.gameObject.SetActive(true);
        promptText.text = curInteractable.GetInteractPrompt();
    }

    public void OnInteractInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && curInteractable != null)
        {
            curInteractable.OnInteract();
            curInteractGameObject = null;
            curInteractable = null;

            promptText.gameObject.SetActive(false);

        }
    }
}
