using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{
    //ī�޶� �������� RAY�� �� �󸶳� ���� ������Ʈ�ؼ� ��������.

    public float checkRate = 0.05f;
    private float lastCheckTime;
    public float maxCheckDistance;
    public LayerMask layerMask; //� ���̾ �޷��ִ� ���ӿ�����Ʈ�� �����Ұ���?

    public GameObject curInteractGameObject; //���ͷ��� �����ߴٸ� �� ���ӿ�����Ʈ ������ ��������.
    private IInteractable curInteractable; //�������̽� ĳ��

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

            if (Physics.Raycast(ray, out hit, maxCheckDistance, layerMask)) //ray���� ����ְ�, ���� �ִ� hit���ٰ� ������ �Ѱ��ش�. ������ maxCheckDistance, layerMask�����ֱ�
            {
                if (hit.collider.gameObject != curInteractGameObject) //������ ���� Interact�ϰ��ִ� ��ü�� �̹� ������ ���� �ִµ�, �װŶ� ���� ������!
                {
                    curInteractGameObject = hit.collider.gameObject;
                    curInteractable = hit.collider.GetComponent<IInteractable>();
                    SetPromptText();
                }
            }
            else //������� ���̸� ���� ���
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
