using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargerEnemyLogics : MonoBehaviour
{
    private Transform player;
    public float speed;
    public float _chargeTime;
    public float steering;

    private float chargeTime;
    private Rigidbody2D rb;
    private Transform tr;
    private Vector2 moveVector;
    private Vector2 moveVelocity;
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
    }

    void FixedUpdate()
    {
        if (chargeTime <= 1 && chargeTime >= 0.8)
        {
            moveVector = (player.position - tr.position).normalized;
            Vector3 diff = player.position - tr.position;
            diff.Normalize();
            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            tr.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
        }
        if (chargeTime <= 0)
        {
            rb.AddForce(moveVector * speed, ForceMode2D.Impulse);
            chargeTime = _chargeTime;
        }
        rb.velocity = Vector3.MoveTowards(rb.velocity, new Vector3(0, 0, 0), Time.fixedDeltaTime * steering);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        antielement = gameObject.GetComponent<Namer>().antielement;
        if (collision.gameObject.tag == "Sword" && player.gameObject.GetComponent<Player>().curElem == antielement)
        {
            Destroy(gameObject);
        }
    }
}
