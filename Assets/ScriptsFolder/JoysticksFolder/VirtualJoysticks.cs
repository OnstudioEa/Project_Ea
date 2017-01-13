using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class VirtualJoysticks : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private Image bgImg;
    public Image joystickImg;
    public Vector3 inputVector { set; get; }
    public Motor motor;

    public PlayerControl playerControl;

    private void Awake()
    {
        bgImg = GetComponent<Image>();
        joystickImg = transform.GetChild(0).GetComponent<Image>();
        inputVector = Vector3.zero;
    }
    public virtual void OnDrag(PointerEventData ped)
    {
        if (playerControl.skillCheck == false)
        {
            if (playerControl.ani.GetBool("AttackMove") == true)
            {
                if (playerControl.attackCheck == false)
                {
                    playerControl.attackCheck = true;
                    playerControl.hit_1.gameObject.SetActive(false);
                }
                motor.state = Motor.State.move;

                playerControl.ani.SetBool("Move", true);

                Vector2 pos = Vector2.zero;
                if (RectTransformUtility.ScreenPointToLocalPointInRectangle(bgImg.rectTransform, ped.position, ped.pressEventCamera, out pos))
                {
                    pos.x = (pos.x / bgImg.rectTransform.sizeDelta.x);
                    pos.y = (pos.y / bgImg.rectTransform.sizeDelta.y);

                    float x = (bgImg.rectTransform.pivot.x == 1) ? pos.x * 2 + 1 : pos.x * 2 - 1;
                    float y = (bgImg.rectTransform.pivot.y == 1) ? pos.y * 2 + 1 : pos.y * 2 - 1;

                    inputVector = new Vector3(x, 0, y);
                    inputVector = (inputVector.sqrMagnitude > 1) ? inputVector.normalized : inputVector;

                    joystickImg.rectTransform.anchoredPosition = new Vector3(inputVector.x * (bgImg.rectTransform.sizeDelta.x * 0.3f)
                                                                            , inputVector.z * (bgImg.rectTransform.sizeDelta.y * 0.3f)); //스틱이동범위
                }
            }
        }            
    }
    public virtual void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped);
    }
    public virtual void OnPointerUp(PointerEventData ped)
    {
        playerControl.ani.SetBool("Move", false);
        motor.state = Motor.State.idle;
        inputVector = Vector3.zero;
        joystickImg.rectTransform.anchoredPosition = Vector3.zero;
    }
}
