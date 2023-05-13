using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    protected GameObject _player;
    protected BoxCollider2D blockRig;
    protected SpriteRenderer image;
    protected Rigidbody2D Rig;
    protected FinalMovement mv;
    public int mode; // -1ÍêÈ«Òþ²Ø 0ÆÕÍ¨ 1Òþ²Ø 2Ëé 3ÏÈÒþ²ØºóËé 4»¬ÁË
    public int mode1; // 0ÆÕÍ¨ 1Òþ²Ø 2Ëé 3ÏÈÒþ²ØºóËé
    public int mode2; // 0ÆÕÍ¨ 1Òþ²Ø 2Ëé 3ÏÈÒþ²ØºóËé
    public float dx,dy;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        mv = _player.GetComponent<FinalMovement>();
        blockRig = GetComponent<BoxCollider2D>();
        Rig = GetComponent<Rigidbody2D>();
        image = GetComponent<SpriteRenderer>();
        mv.blocks.Add(this);
        dx = transform.position.x;
        dy = transform.position.y;

        reset();
    }

    // Update is called once per frame
    void Update()
    {
        if (mv.back == 0) mode = mode1;
        else if (mv.back == 1) mode = mode2;
    }

    public void reset()
    {
        Rig.bodyType = RigidbodyType2D.Kinematic;
        transform.position = new Vector3(dx, dy, 0f);
        Rig.velocity = new Vector3(0, 0, 0f);
        Rig.rotation = 0f;
        Rig.angularVelocity = 0f;
        Debug.Log(Rig.rotation);
        if (mode == -1)
        {
            image.enabled = false;
            blockRig.isTrigger = true;
        }
        else if (mode == 0)
        {
            blockRig.enabled = true;
        }
        else if (mode == 1)
        {
            image.enabled = false;
            blockRig.isTrigger = true;
        }
        else if (mode == 2)
        {
            blockRig.enabled = true;
        }
        else if (mode == 3)
        {
            image.enabled = false;
            blockRig.isTrigger = true;
        }
        else if (mode == 4)
        {
            image.enabled = true;
            blockRig.isTrigger = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var playerPos = _player.GetComponent<Transform>().position;
        var curPos = transform.position;
        if (mode==2 && collision.collider.CompareTag("Player") &&  playerPos.y - curPos.y > 0.5)
        {
            //Debug.Log("!!!");
            blockRig.enabled = false;
            Rig.bodyType = RigidbodyType2D.Dynamic;
        }
        if (mode == 4 && collision.collider.CompareTag("Player") && playerPos.y - curPos.y > 0.5)
        {
            //Debug.Log("!!!");
            Rig.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var playerPos = _player.GetComponent<Transform>().position;
        var curPos = transform.position;
        if (mode == 1 && collision.CompareTag("Player") && playerPos.y - curPos.y < -0.5 && _player.GetComponent<FinalMovement>().rb.velocity.y >0f)
        {
            Debug.Log("hidden -> normal");
            image.enabled = true;
            blockRig.isTrigger = false;
            mode = 0;
        }
        else if (mode == 3 && collision.CompareTag("Player") && playerPos.y - curPos.y < -0.5 && _player.GetComponent<FinalMovement>().rb.velocity.y > 0f)
        {
            Debug.Log("hiddenbroken -> broken");
            image.enabled = true;
            blockRig.isTrigger = false;
            mode = 2;
        }
    }
}
