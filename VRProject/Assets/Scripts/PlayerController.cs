using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum EnterExitRay
{
    Enter = 0,
    Stay,
    Exit,
    No
};
public class PlayerController : MonoBehaviour
{
    public GameObject leftDoor, rightDoor;
    public GameObject elevatorUI;

    Outline lineBefore; //최근에 outline 그려진 오브젝트
    UIController uiController;
    RaycastHit hit;
    float MaxDistance = 10f;
    float h, v, h1, v1; //각각 수평 입력, 수직 입력, 마우스 수평, 마우스 수직
    float speed = 3.0f; //이동 속도
    float cameraSpeed = 2.0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; //마우스 제거
        uiController = GetComponent<UIController>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && (speed != 0)) //space 입력 시 이동 속도 빠르게, UIMode ON일시 못 움직이게
            speed = 6.0f;
        else if (speed != 0)
            speed = 3.0f;

        if (Input.GetKey(KeyCode.E)) //E 입력시 메뉴 열림
            uiController.UIModeOn(UIType.Menu);

        if (lineBefore != null) //윤곽선 지우기
        {
            lineBefore.enabled = false;
            lineBefore = null;
            uiController.SetInteraction(false);
        }
        if (Physics.Raycast(transform.position, transform.forward * -1, out hit, MaxDistance)) //보면 윤곽선 그리기
        {
            Outline outline = hit.transform.GetComponent<Outline>();
            if (outline != null) //윤곽선 그리기
            {
                lineBefore = outline; //윤곽선 시야에서 벗어나면 지우기 위해서 저장해놓기
                outline.enabled = true;
                uiController.SetInteraction(true);
            }

            if (outline != null && Input.GetKey(KeyCode.F)) //상호작용 시
            {
                //추후 변경
                //InteractionObject inter = hit.transform.GetComponent<InteractionObject>(); 
                //inter.Interact();

                if (hit.transform.CompareTag("Elevator")) //임시
                {
                    uiController.UIModeOn(UIType.Elevator);
                }
            }
        }
        //Debug.DrawRay(transform.position, transform.forward * MaxDistance * -0.95f, Color.blue, 0.3f);
        //Debug.DrawRay(transform.position, transform.forward * MaxDistance * -1.05f, Color.blue, 0.3f);

        h = Input.GetAxis("Horizontal") * speed * Time.deltaTime; //수평 입력 + deltatime : 프레임에 따른 이동속도 차이 보완
        v = Input.GetAxis("Vertical") * speed * Time.deltaTime;

        h1 = Input.GetAxis("Mouse X") * cameraSpeed; //마우스 움직임 입력
        v1 = Input.GetAxis("Mouse Y") * cameraSpeed;

        transform.Rotate(0, h1, 0); //마우스 이동
        transform.Translate(-h, 0, -v); //플레이어 이동
    }

    public void SetSpeedUI(bool isUI) //멀티플레이니까 시간 멈추지 않고 속도 0으로 만들기
    {
        if (isUI)
        {
            speed = cameraSpeed = 0f;
        }
        else
        {
            speed = 3.0f;
            cameraSpeed = 2.0f;
        }
    }

    private void OnTriggerEnter(Collider other) //트리거 충돌(실제로 충돌하지는 않지만 신호만 옴)
    {
        if (other.CompareTag("Elevator")) //엘리베이터 동작
        {
            //elevatorUI.SetActive(true);
            StartCoroutine("Open");
        }
    }

    private void OnTriggerExit(Collider other) //트리거 충돌 해제
    {
        if (other.CompareTag("Elevator"))
        {
            //elevatorUI.SetActive(false);
            StartCoroutine("Close");
        }
    }

    IEnumerator Open() //엘리베이터 문 여는 코루틴
    {
        for (float f = 2f; f >= 0; f -= 0.01f)
        {
            leftDoor.transform.Translate(0.01f, 0, 0);
            rightDoor.transform.Translate(-0.01f, 0, 0);
            yield return new WaitForEndOfFrame();
        }
    }
    IEnumerator Close()
    {
        for (float f = 2f; f >= 0; f -= 0.01f)
        {
            leftDoor.transform.Translate(-0.01f, 0, 0);
            rightDoor.transform.Translate(0.01f, 0, 0);
            yield return new WaitForEndOfFrame();
        }
    }
}
