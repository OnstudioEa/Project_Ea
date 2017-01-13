using UnityEngine;
using System.Collections;

public class SanctumScript : MonoBehaviour {

    public UIPanel start_Panel;
    public UIPanel selectStage_Panel;
    //public UIPanel common_Panel;
    public UIPanel stage_Panel;
    public UIPanel world_Panel;
    public UIPanel dungeon_Panel;
    

	void Awake () {
        
        start_Panel.gameObject.SetActive(true);
        selectStage_Panel.gameObject.SetActive(false);
        stage_Panel.gameObject.SetActive(false);
        world_Panel.gameObject.SetActive(false);
        dungeon_Panel.gameObject.SetActive(false);

	}
    public void StageStartButton()
    {
        start_Panel.gameObject.SetActive(false);
        selectStage_Panel.gameObject.SetActive(true);
        world_Panel.gameObject.SetActive(true);
    }
    public void Stage_1Button()
    {
        world_Panel.gameObject.SetActive(false);
        stage_Panel.gameObject.SetActive(true);
        //selectStage_Panel.gameObject.SetActive(false);
        //dungeon_Panel.gameObject.SetActive(true);
    }
    public void DungeonButton()
    {
        stage_Panel.gameObject.SetActive(false);
        dungeon_Panel.gameObject.SetActive(true);
    }
}
