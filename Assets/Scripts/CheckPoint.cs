using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{

    protected GameObject _player;
    protected SpriteRenderer image;
    protected GameObject ep;
    public float x, y;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        image = GetComponent<SpriteRenderer>();
        ep  = GameObject.FindGameObjectWithTag("EndPoint");
        x = transform.position.x;
        y = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        _CheckPlayerPos();
    }

    private void _CheckPlayerPos()
    {
        var playerPos = _player.GetComponent<Transform>().position;
        var curPos = transform.position;
        if ((playerPos.y - curPos.y) * (playerPos.y - curPos.y) + (playerPos.x - curPos.x) * (playerPos.x - curPos.x) < 1.0)
        {
            //isVisible = false;
            image.enabled = false;
            _player.GetComponent<FinalMovement>().startx = x;
            _player.GetComponent<FinalMovement>().starty = y;
            _player.GetComponent<FinalMovement>().back = 1;
            ep.GetComponent<EndPoint>().cpl.Add(this);
        }
    }
}
