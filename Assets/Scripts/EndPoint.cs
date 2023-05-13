using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPoint : MonoBehaviour
{
    protected GameObject _player;
    protected SpriteRenderer image;
    public List<CheckPoint> cpl = new List<CheckPoint>();
    public float x, y;
    public int cnt;
    public int sceneName;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        image = GetComponent<SpriteRenderer>();
        x = transform.position.x;
        y = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(cpl.Count >= cnt)
        {
            image.enabled = true;
            _CheckPlayerPos();
        }
        else
        {
            image.enabled = false;
        }
    }

    private void _CheckPlayerPos()
    {
        var playerPos = _player.GetComponent<Transform>().position;
        var curPos = transform.position;
        if ((playerPos.y - curPos.y) * (playerPos.y - curPos.y) + (playerPos.x - curPos.x) * (playerPos.x - curPos.x) < 1.0)
        {
            //isVisible = false;
            image.enabled = false;
            SceneManager.LoadScene(sceneName);//括号内加入场景名字
        }
    }
}
