using UnityEngine;
using System.Collections;

public class Manage : MonoBehaviour {

    private static Manage instance;
    public static Manage Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<Manage>();
            return instance;
        }
    }
    public Canvas _Canvas;
    public ClonePool _ClonePool;
    public SceneTransition _Win;
    public SceneTransition _Lose;
    public static int _Level;
    public static int _Star;
    public static int _LastStar;
    public float _Delay;
    public TextAsset[] _config;
    public int _TotalScore;
    
    void Start()
    {
        if (!PlayerPrefs.HasKey("PlayLevel"))
            return;
        int level = PlayerPrefs.GetInt("PlayLevel");
        _Level = level;
        _LastStar = PlayerPrefs.GetInt("Level-"+ _Level);
        for (int i = 0; i < _config.Length; i++)
        {
            if(int.Parse(_config[i].name)== level)
            {
                Create_Game(_config[i].text);
                return;
            }
        }
    }
	public void SceneTransition_Lose()
    {
        Invoke("Delay_SceneTransition_Lose", _Delay);
    }
    void Delay_SceneTransition_Lose()
    {
        _Lose.PerformTransition();
    }
    public void SceneTransition_Win(int _star)
    {
        _Star = _star;
        if (_Star > _LastStar)
        {
            PlayerPrefs.SetInt("Level-" + _Level, _Star);
        }
        if (_LastStar == 0)
        {
            int nextLevel = _Level+1;
            //if (!PlayerPrefs.HasKey("Level-" + nextLevel))
            PlayerPrefs.SetInt("Level-" + nextLevel, 0);
        }
        Invoke("Delay_SceneTransition_Win", _Delay);
    }
    void Delay_SceneTransition_Win()
    {
        _Win.PerformTransition();
    }
    public GameObject Create_Wall()
    {
        GameObject go = Instantiate(_ClonePool.Wall, _Canvas.transform, false) as GameObject;
        go.SetActive(true);
        return go;
    }
    public GameObject Create_Stick()
    {
        GameObject go = Instantiate(_ClonePool.Stick, _Canvas.transform, false) as GameObject;
        go.SetActive(true);
        return go;
    }
    public GameObject Create_Ball()
    {
        GameObject go = Instantiate(_ClonePool.Ball, _Canvas.transform, false) as GameObject;
        go.SetActive(true);
        return go;
    }
    public GameObject Create_Star()
    {
        GameObject go = Instantiate(_ClonePool.Star, _Canvas.transform, false) as GameObject;
        go.SetActive(true);
        return go;
    }
    public string Sava_Config()
    {
        Ball []ball= GameObject.FindObjectsOfType<Ball>();
        Wall[] Wall = GameObject.FindObjectsOfType<Wall>();
        Stick[] Stick = GameObject.FindObjectsOfType<Stick>();
        Star[] Star = GameObject.FindObjectsOfType<Star>();
        string _config = "";
        for (int i = 0; i < ball.Length; i++)
        {
            _config += PrintfConfigStr("Ball", ball[i].gameObject);
        }
        for (int i = 0; i < Wall.Length; i++)
        {
            _config += PrintfConfigStr("Wall", Wall[i].gameObject);
        }
        for (int i = 0; i < Stick.Length; i++)
        {
            _config += PrintfConfigStr("Stick", Stick[i].gameObject);
        }
        for (int i = 0; i < Star.Length; i++)
        {
            _config += PrintfConfigStr("Star", Star[i].gameObject);
        }
        return _config;
    }
    string PrintfConfigStr(string _name, GameObject go)
    {
        string str = _name;
        str += "|";
        str += go.transform.localPosition.x + "," + go.transform.localPosition.y + "," + go.transform.localPosition.z;
        str += "|";
        str += Mathf.RoundToInt(go.transform.localEulerAngles.x) + "," + Mathf.RoundToInt(go.transform.localEulerAngles.y) + "," + Mathf.RoundToInt(go.transform.localEulerAngles.z);
        str += "|";
        str += go.transform.localScale.x + "," + go.transform.localScale.y + "," + go.transform.localScale.z;
        str += "|";
        str += go.GetComponent<RectTransform>().rect.width + "," + go.GetComponent<RectTransform>().rect.height;
        str += "#";
        return str;
    }
    public void Delete_Game()
    {
        Ball[] ball = GameObject.FindObjectsOfType<Ball>();
        Wall[] Wall = GameObject.FindObjectsOfType<Wall>();
        Stick[] Stick = GameObject.FindObjectsOfType<Stick>();
        Star[] Star = GameObject.FindObjectsOfType<Star>();
        foreach(var obj in ball)
        {
            DestroyImmediate(obj.gameObject); 
        }
        foreach (var obj in Wall)
        {
            DestroyImmediate(obj.gameObject);
        }
        foreach (var obj in Stick)
        {
            DestroyImmediate(obj.gameObject);
        }
        foreach (var obj in Star)
        {
            DestroyImmediate(obj.gameObject);
        }
    }
    public void Create_Game(string _config)
    {
        Delete_Game();
        string []objs = _config.Split('#');
        for (int i = 0; i < objs.Length; i++)
        {
            Create_Obj(objs[i].Split('|'));
        }

        if (Application.isPlaying)
        {
            Invoke("SetTotalScore", 0.1f);
        }
    }
    void Create_Obj(string[] _objConfig)
    {
        switch (_objConfig[0])
        {
            case "Ball":
                GameObject Ball = Create_Ball();
                Set_Obj_info(_objConfig, Ball);
                break;
            case "Wall":
                GameObject Wall = Create_Wall();
                Set_Obj_info(_objConfig, Wall);
                break;
            case "Stick":
                GameObject Stick = Create_Stick();
                Set_Obj_info(_objConfig, Stick);
                break;
            case "Star":
                GameObject Star = Create_Star();
                Set_Obj_info(_objConfig, Star);
                break;
            default:
                break;
        }
    }
    void Set_Obj_info(string[] _objConfig,GameObject go)
    {
        RectTransform _RectTransform = go.GetComponent<RectTransform>();
        string[] _localPosition = _objConfig[1].Split(',');
        string[] _localEulerAngles = _objConfig[2].Split(',');
        string[] _localScale = _objConfig[3].Split(',');
        string[] _rect = _objConfig[4].Split(',');
        Vector3 localPosition = new Vector3(float.Parse(_localPosition[0]), float.Parse(_localPosition[1]), float.Parse(_localPosition[2]));
        Vector3 localEulerAngles = new Vector3(float.Parse(_localEulerAngles[0]), float.Parse(_localEulerAngles[1]), float.Parse(_localEulerAngles[2]));
        Vector3 localScale = new Vector3(float.Parse(_localScale[0]), float.Parse(_localScale[1]), float.Parse(_localScale[2]));
        Vector2 rect = new Vector2(float.Parse(_rect[0]), float.Parse(_rect[1]));
        go.transform.localPosition = localPosition;
        go.transform.localEulerAngles = localEulerAngles;
        go.transform.localScale = localScale;
        _RectTransform.sizeDelta = rect;
    }
    void SetTotalScore()
    {
        Rigidbody2D[] _Rigidbody2D = GameObject.FindObjectsOfType<Rigidbody2D>();
        for (int i = 0; i < _Rigidbody2D.Length; i++)
        {
            _Rigidbody2D[i].isKinematic = false;
        }
        _TotalScore = GameObject.FindObjectsOfType<Star>().Length;
    }
}
