using UnityEngine;
using System.Collections;

public class LevelGameInfo : MonoBehaviour {

    public TextAsset[] configs;
    public int level;
    public TextAsset config;
    public int star;
    void Awake()
    {
        PlayerPrefs.SetInt("Level-1",2);
        PlayerPrefs.SetInt("Level-2", 0);
    }
    void Start () {
        //DontDestroyOnLoad(gameObject);
	}

}
