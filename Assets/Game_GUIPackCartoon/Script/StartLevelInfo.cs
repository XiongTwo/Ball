using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartLevelInfo : MonoBehaviour {

    public Text title;
    public int level;
    public int star;
    
    public void SetPlayLevelInfo()
    {
        PlayerPrefs.SetInt("PlayLevel", level);
    }
}
