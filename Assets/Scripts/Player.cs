using Newtonsoft.Json.Schema;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

enum elem
{
    None,
    ROCK,
    PAPER,
    SCISSORS
}

public class Player : MonoBehaviour
{
    public int maxhp;
    public float speed;
    public float steering;
    public TextMeshProUGUI hpbar;
    public TextMeshProUGUI elementbar;
    public GameObject death;   

    private string[] elemNames = { "No element", "Rock", "Paper", "Scissors" };
    private int _curElem;
    private int hp;
    private Rigidbody2D rbody;
    private Vector2 moveInput;
    private Vector2 moveVelocity;
    private SpriteRenderer[] sprites;

    public int curElem
    {
        get { return _curElem; }
        set { if (value >= 0 && value <= 3) { _curElem = value; elementbar.text = elemNames[value]; } }
    }

    void Start()
    {
        death.SetActive(false); 
        _curElem = ((int)elem.None);
        hp = maxhp;
        rbody = GetComponent<Rigidbody2D>();
        sprites = GetComponentsInChildren<SpriteRenderer>();
        hpbar.text = "HP: " + hp;
    }

    void Update()
    {
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveVelocity = Vector3.Lerp(moveVelocity, moveInput.normalized * speed, steering * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.Space)) TakeDamage(); //��������� ��������� ����� �� ������
    }

    private void FixedUpdate()
    {
        rbody.MovePosition(rbody.position + moveVelocity * Time.fixedDeltaTime);
    }

    async void TakeDamage()
    {
        hp -= 1;
        hpbar.text = "HP: " + hp;
        if (hp == 0) {
            Time.timeScale = 0;
            death.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Area")
            curElem = collision.gameObject.GetComponent<ChangeElement>().element;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
            TakeDamage();
    }
}