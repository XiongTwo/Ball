using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

    private RectTransform _CanvasRectTransform;
    private RectTransform _BallRectTransform;
    private ClonePool _ClonePool;
    private float _DieDot;
    private float _Score;
    void Awake () {
        _CanvasRectTransform = transform.parent.GetComponent<RectTransform>();
        _BallRectTransform= gameObject.GetComponent<RectTransform>();
        _ClonePool = GameObject.FindObjectOfType<ClonePool>();
    }
    void Start()
    {
        _DieDot= -1*(_CanvasRectTransform.rect.height / 2f + _BallRectTransform.rect.height * _BallRectTransform.localScale.y);
    }
    void Update()
    {
        if (IsDie())
            Die();
    }
    bool IsDie()
    {
        if (_BallRectTransform.localPosition.y < _DieDot)
            return true;
        return false;
    }
    void Die()
    {
        Destroy(gameObject);
        if (_Score <= 0)
            Manage.Instance.SceneTransition_Lose();
        else
        {
            
            float val = _Score / Manage.Instance._TotalScore;
            int num = 0;
            if (val < 0.6)
                num = 1;
            else if (val < 1)
                num = 2;
            else if (val >= 1)
                num = 3;
            Manage.Instance.SceneTransition_Win(num);
        }
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        _Score++;
        GameObject go = Instantiate(_ClonePool.ConfettiStars_Star, _BallRectTransform.parent, false) as GameObject;
        go.transform.position = coll.transform.position;
        go.SetActive(true);
        Destroy(coll.gameObject);     
    }
}
