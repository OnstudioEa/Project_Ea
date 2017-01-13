using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 실질적인 외부인벤토리
/// </summary>
[SerializeField] //직렬화
public class SaveInven 
{
    public string itemList;
}
/// <summary>
/// 게임 내부에서 쓰이는 인벤토리
/// </summary>
public class Inven
{
    public List<Inven_Item> _lstItme;


    public Inven()
    {
        _lstItme = new List<Inven_Item>();
    }
}

public class Inven_Item
{
    public int unigueNo;
    public int count;
}

public enum Inven_Item_Type
{
    [StringValue("None")] None = 0,

    [StringValue("M_A")]  Material_A  = 1000,
    [StringValue("M_B")]  Material_B  = 1001,
    [StringValue("M_C")]  Material_C  = 1002,
                          
    [StringValue("W_A")]  Weapon_A    = 2000,
    [StringValue("W_B")]  Weapon_B    = 2001,
    [StringValue("W_C")]  Weapon_C    = 2002,
                          
    [StringValue("A_A")]  Armor_A     = 3000,
    [StringValue("A_B")]  Armor_B     = 3001,
    [StringValue("A_C")]  Armor_C     = 3002,
}

public class TestCode_SaveManager : Singleton<TestCode_SaveManager>
{
    Inven _inven;
    public Inven Inven { get { return _inven; } }
    
    /// <summary>
    /// 인벤토리 초기화
    /// </summary>
    public void Initialize()
    {
        _inven = new Inven();
        if (!InvenLoad(PlayerPrefs.GetString("Inven")))
        {
            Debug.Log("인벤토리가 없거나 불러올 수 없습니다.");
        }
    }

    /// <summary>
    /// 아이템을 더함
    /// </summary>
    /// <param name="type"></param>
    /// <param name="count"></param>
    public void Add(Inven_Item_Type type, int count = 1)
    {
        for (int i = 0; i < _inven._lstItme.Count; i++)
        {
            if (_inven._lstItme[i].unigueNo == (int)type)
            {
                _inven._lstItme[i].count += count;
                return;
            }
        }
        Inven_Item item = new Inven_Item();
        item.unigueNo = (int)type;
        item.count += count;

        _inven._lstItme.Add(item);
    }

    /// <summary>
    /// 아이템을 더함
    /// </summary>
    /// <param name="type"></param>
    /// <param name="count"></param>
    void Add(int type, int count = 1)
    {
        for (int i = 0; i < _inven._lstItme.Count; i++)
        {
            if (_inven._lstItme[i].unigueNo == type)
            {
                _inven._lstItme[i].count += count;
                return;
            }
        }
        Inven_Item item = new Inven_Item();
        item.unigueNo = type;
        item.count += count;

        _inven._lstItme.Add(item);
    }


    public bool InvenSave()
    {
        SaveInven strInven = new SaveInven();
        strInven.itemList = "";
        for (int i = 0; i < _inven._lstItme.Count; i++)
        {
            strInven.itemList += _inven._lstItme[i].unigueNo.ToString() + ":";
            strInven.itemList += _inven._lstItme[i].count.ToString();
            if (i != _inven._lstItme.Count-1)
            {
                strInven.itemList += ",";
            }
        }
        string strJSON = JsonUtility.ToJson(strInven); //제이슨형식으로 만듬

        PlayerPrefs.SetString("Inven", strJSON);
        Debug.Log(strJSON);
        return true;
    }


    public bool InvenLoad(string strJSON)
    {
        if (strJSON == "" || strJSON == string.Empty || strJSON == null)
        {
            return false;
        }
        SaveInven saveInven = JsonUtility.FromJson<SaveInven>(strJSON); // 제이슨형식에서 데이터로

        string[] arr1 = saveInven.itemList.Split(',');

        _inven = new Inven();
        for (int i = 0; i < arr1.Length; i++)
        {
            string[] arr2 = arr1[i].Split(':');
            
            Add(int.Parse(arr2[0]), int.Parse(arr2[1]));
            Debug.Log("uniqueNo : " + _inven._lstItme[i].unigueNo);
            Debug.Log("count : " + _inven._lstItme[i].count);
        }
        return true;
    }
}
