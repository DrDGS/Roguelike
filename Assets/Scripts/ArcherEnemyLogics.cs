using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherEnemyLogics : MonoBehaviour
{
    public Transform bow;
    public float _chargeTime;
    public GameObject arrow;
    public float arrowSpeed;
    public float steering;
    public float offsetToPlayer;

    private float chargeTime;
    private Transform tr;
    private Rigidbody2D rb;
    private Transform player;
    private bool arrflag = false;
    private GameObject thisarrow;
    private int antielement;

    void Start()
    {
        chargeTime = _chargeTime;
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectsWithTag("Player")[0].transform;
    }

    void Update()
    {
        chargeTime -= Time.deltaTime;
        Vector3 diff = player.position - tr.position;
        if (diff.magnitude > offsetToPlayer)
            rb.velocity = Vector3.MoveTowards(rb.velocity, diff, Time.fixedDeltaTime * steering);
        else if (diff.magnitude < 7.5f)
            rb.velocity = Vector3.MoveTowards(rb.velocity, -diff, Time.fixedDeltaTime * steering);
        else
            rb.velocity = Vector3.MoveTowards(rb.velocity, new Vector3(0, 0, 0), Time.fixedDeltaTime * steering);
            
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        bow.rotation = Quaternion.Euler(0f, 0f, rot_z - 180);
        if (diff.x > 0)
            bow.localPosition = new Vector3(2, 0, -1);
        else
            bow.localPosition = new Vector3(-2, 0, -1);
        if (chargeTime <= 1)
        {
            if (!arrflag)
            {
                thisarrow = Instantiate(arrow, bow.position, Quaternion.identity);
                arrflag = true;
            }
            thisarrow.GetComponent<Transform>().rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
            thisarrow.GetComponent<Transform>().position = bow.position;
        }
        if (chargeTime <= 0)
        {
            chargeTime = _chargeTime;
            arrflag = false;
            thisarrow.GetComponent<Rigidbody2D>().AddForce(Quaternion.Euler(0f, 0f, rot_z - 90) * new Vector3(0, 1, 0) * arrowSpeed);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        antielement = gameObject.GetComponent<Namer>().antielement;
        if (collision.gameObject.tag == "Sword" && player.gameObject.GetComponent<Player>().curElem == antielement)
        {
            player.gameObject.GetComponent<Player>().AddPoints(gameObject.GetComponent<Namer>().points);
            Destroy(gameObject);
        }
    }
}
