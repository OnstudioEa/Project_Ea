using UnityEngine;
using System.Collections;

public class MonsterShaderChange : MonoBehaviour {

    public Material[] material;
    public Renderer rend;

	// Use this for initialization
	void Awake () {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = material[0];
    }
}
