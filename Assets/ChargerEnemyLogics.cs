using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargerEnemyLogics : MonoBehaviour
{
    public Transform player;
    public float speed;
    public float _chargeTime;
    public float steering;

    private float chargeTime;
    private Rigidbody2D rb;
    private Transform tr;
    private Vector2 moveVector;
    private Vector2 moveVelocity;

    void Start()
    {
        chargeTime = _chargeTime;
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        chargeTime -= Time.deltaTime; 
    }

    void FixedUpdate()
    {
        if (chargeTime <= 2)
        {
            Vector3 diff = player.position - tr.position;
            diff.Normalize();
            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            tr.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
        }
        if (chargeTime <= 0)
        {
            moveVector = (player.position - tr.position).normalized;
            rb.AddForce(moveVector * speed, ForceMode2D.Impulse);
            chargeTime = _chargeTime;
        }
        rb.velocity = Vector3.MoveTowards(rb.velocity, new Vector3(0, 0, 0), Time.fixedDeltaTime * steering);
    }
}
