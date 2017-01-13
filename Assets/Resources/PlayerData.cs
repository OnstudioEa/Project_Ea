using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;

public class PlayerData {
    [XmlElement("Level")]
    public int level;
    
    [XmlElement("Power")]
    public int power;

    [XmlElement("PlayerHP")]
    public int playerHp;

    [XmlElement("MonsterLevel")]
    public int monsterLevel;

    [XmlElement("MonsterHp")]
    public int monsterHp;

    [XmlElement("MonsterPower")]
    public int monsterPower;
}

