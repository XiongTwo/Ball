using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Level : MonoBehaviour {

    public string _Key;
    public GameObject _Level;
    public GameObject _New;
    public GameObject _Lock;
    public Text[] _Text;
    public GameObject[] _Star;
    public Sprite grey;
    public Sprite yellow;
    void Start () {
        _Key = "Level-" + gameObject.name;
        for (int i = 0; i < _Text.Length; i++)
        {
            _Text[i].text = gameObject.name;
        }
        if (!PlayerPrefs.HasKey(_Key))
        {
            if (gameObject.name != "1")
                _Lock.SetActive(true);
            else
            {
                _New.SetActive(true);
                _New.GetComponentInChildren<PlayPopupOpener>().level = int.Parse(gameObject.name);
            }
        }
        else
        {
            int _starNum = PlayerPrefs.GetInt(_Key);
            if (_starNum == 0)
            {
                _New.SetActive(true);
                _New.GetComponentInChildren<PlayPopupOpener>().level = int.Parse(gameObject.name);
            }
            else
            {
                _Level.SetActive(true);
                _Level.GetComponentInChildren<PlayPopupOpener>().level = int.Parse(gameObject.name);
                _Level.GetComponentInChildren<PlayPopupOpener>().starsObtained = _starNum;
                for (int i = 0; i < _Star.Length; i++)
                {
                    _Star[i].GetComponent<Image>().sprite = grey;
                }
                for (int i = 0; i < _starNum; i++)
                {
                    _Star[i].GetComponent<Image>().sprite = yellow;
                }
            }
        }
	}
}
