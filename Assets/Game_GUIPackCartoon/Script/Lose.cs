using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Lose : MonoBehaviour {

    public Text _Level;
    
    void Awake () {
        _Level.text = "Level " + Manage._Level;
    }
}
