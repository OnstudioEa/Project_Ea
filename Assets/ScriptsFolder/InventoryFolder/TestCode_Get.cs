using UnityEngine;
using System.Collections;

public class TestCode_Get : MonoBehaviour {

	// Use this for initialization
	void Start () {
        TestCode_SaveManager.Instance.Initialize();
    }
	

    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 30), "AddItem_W_A"))
        {
            GetItem(Inven_Item_Type.Weapon_A);
        }
        if (GUI.Button(new Rect(10, 50, 100, 30), "AddItem_A_A"))
        {
            GetItem(Inven_Item_Type.Armor_A);
        }
        if (GUI.Button(new Rect(10, 90, 100, 30), "AddItem_M_A"))
        {
            GetItem(Inven_Item_Type.Material_A);
        }
        if (GUI.Button(new Rect(10, 130, 100, 30), "Save"))
        {
            SaveInven();
        }
    }
    public void GetItem(Inven_Item_Type type, int count = 1)
    {
        TestCode_SaveManager.Instance.Add(type, count);
    }

    public void SaveInven()
    {
        TestCode_SaveManager.Instance.InvenSave();
    }

    public void ResetInven()
    {
        TestCode_SaveManager.Instance.Initialize();
    }

    // Update is called once per frame
    void Update () {
	
	}
}
