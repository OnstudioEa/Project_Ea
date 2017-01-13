using UnityEngine;
using System.Collections;

/// 

/// Item 클래스. 
/// 각 아이템은 이 클래스를 가지고 있습니다.
/// 

public class ItemScript : MonoBehaviour
{

    // 아이템 이름. 구분 짓기 위해 만들어요.
    private string m_strName;
    // 아이콘을 표시할 스프라이트 이름입니다.
    private string m_strSpriteName;
    // 아이콘을 표시하는 Sprite 클래스 입니다.
    // 여기에 아이템 아이콘 이미지 세팅 할꺼에요.
    public UISprite m_sprIcon;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    // 정보를 설정하는 함수 입니다.
    public void SetInfo(string spriteName)
    {
        // 같은 아틀라스에 있으니 스프라이트 이름 찾아 넣어주면 이미지가 바껴요.
        m_sprIcon.spriteName = spriteName;
        // 이름도 설정 합시다.(확인 위해 이름설정하는거)
        m_strName = spriteName;
    }

    // 터치 하면 발생하는 이벤트입니다.
    // 전에 Button을 썼지만 OnClick으로 사용하겠습니다.
    // OnClick은 NGUI에서 제공하는 함수로 터치하면 발생됩니다.
    void OnClick()
    {
        // 확인 위해 로그 찍어봅니다.
        Debug.Log(m_strName + " 이 클릭되었습니다.");
    }
}
