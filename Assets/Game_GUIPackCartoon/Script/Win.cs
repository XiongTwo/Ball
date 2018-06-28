using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Win : MonoBehaviour {

    public Text _Level;
    public GameObject[] _Stars;
    public SceneTransition _SceneTransition;

    void Awake()
    {
        _Level.text = "Level " + Manage._Level;
        Invoke("ShowStar",0.5f);
    }
    void ShowStar()
    {
        var _Star = Manage._Star;
        for (int i = 0; i < _Star; i++)
        {
            _Stars[i].SetActive(true);
        }
    }
    public void NextButton()
    {
        int _PlayLevel = PlayerPrefs.GetInt("PlayLevel");
        if (_PlayLevel < 4)
        {
            _PlayLevel++;
            PlayerPrefs.SetInt("PlayLevel", _PlayLevel);
            _SceneTransition.scene = "Game";
        }
        else
        {
            _SceneTransition.scene = "Level";
        }
    }
}
