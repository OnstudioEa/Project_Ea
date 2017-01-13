using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("DataCollection")]
public class PlayerDataContainer {
    [XmlArray("GameData")]

  //  public List<MonsterData> monsterData = new List<MonsterData>();
    public List<PlayerData> playerData = new List<PlayerData>();

    public static PlayerDataContainer Load(string path)
    {
        TextAsset _xml = Resources.Load<TextAsset>(path);

        XmlSerializer serializer = new XmlSerializer(typeof(PlayerDataContainer));

        StringReader reader = new StringReader(_xml.text);

        PlayerDataContainer manager = serializer.Deserialize(reader) as PlayerDataContainer;

        reader.Close();

        return manager;
    }
}
