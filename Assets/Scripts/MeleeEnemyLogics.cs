using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList.Element_Adder_Menu;
using UnityEngine;

public class MeleeEnemyLogics : MonoBehaviour
{
    private Transform player;
    public float speed;
    public float steering;

    private Rigidbody2D rb;
    private Transform tr;
    private Vector2 moveVector;
    private Vector2 moveVelocity;
    private int antielement;

    void Start()
    {
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectsWithTag("Player")[0].transform;
    }

    void Update()
    {
        moveVector = Vector3.MoveTowards(tr.position, player.position, speed / 100) - tr.position;
        moveVelocity = Vector3.Lerp(moveVelocity, moveVector.normalized * speed, steering * Time.deltaTime);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
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
