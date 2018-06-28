using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour {

    private RectTransform _RectTransform;
    private BoxCollider2D _BoxCollider2D;
    private ClonePool _ClonePool;
    private float _width;
    private float _height;
    void Start()
    {
        _RectTransform = gameObject.GetComponent<RectTransform>();
        _width = _RectTransform.rect.width;
        _height = _RectTransform.rect.height;
        _BoxCollider2D = gameObject.GetComponent<BoxCollider2D>();
        _BoxCollider2D.size = new Vector2(_width, _height);
        _ClonePool = GameObject.FindObjectOfType<ClonePool>();
    }
    /*public void OnClick()
    {
        GameObject go = Instantiate(_ClonePool.ConfettiStars_Wall,_RectTransform.parent,false) as GameObject;
        go.transform.position = gameObject.transform.position;
        go.SetActive(true);
        Destroy(gameObject);
    }*/
}
