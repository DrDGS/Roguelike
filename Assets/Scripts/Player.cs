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
    public TextMeshProUGUI timerbar;
    public TextMeshProUGUI pointsbar;
    public GameObject death;
    public GameObject win;
    public GameObject absVic;
    public float[] spawnRate;
    public int[] plusEnemies;
    public float[] gameTime;

    private float gameTimeInSeconds;
    private Spawner spawner;
    private LevelSystem levSys;
    private int points;
    private float timeLeft;
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
        Time.timeScale = 1;
        death.SetActive(false);
        win.SetActive(false);
        absVic.SetActive(false);
        levSys = win.GetComponent<LevelSystem>();
        spawner = GetComponent<Spawner>();
        spawner.SpawnRate = spawnRate[levSys.level];
        spawner.PlusEnemies = plusEnemies[levSys.level];
        gameTimeInSeconds = gameTime[levSys.level];
        timeLeft = gameTimeInSeconds;
        _curElem = ((int)elem.None);
        hp = maxhp;
        rbody = GetComponent<Rigidbody2D>();
        sprites = GetComponentsInChildren<SpriteRenderer>();
        hpbar.text = "HP: " + hp;
        AddPoints(win.GetComponent<LevelSystem>().points);
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
        timerbar.text = ((int)timeLeft) / 60 + ":" + ((int)timeLeft) % 60;
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveVelocity = Vector3.Lerp(moveVelocity, moveInput.normalized * speed, steering * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.Space)) TakeDamage();
        if (timeLeft <= 0)
        {
            if (levSys.level == 4)
            {
                levSys.level = 0;
                levSys.points = 0;
                Time.timeScale = 0;
                absVic.SetActive(true);
            }
            else if (levSys.level <= 3 && !absVic.activeInHierarchy)
            {
                levSys.points = points;
                Time.timeScale = 0;
                win.SetActive(true);
            }
        }
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
            levSys.level = 0;
            levSys.points = 0;
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
        else if (collision.gameObject.tag == "Projectile")
        {
            TakeDamage();
            GameObject.Destroy(collision.gameObject);
        }
    }

    public void AddPoints(int _points)
    {
        points += _points;
        pointsbar.text = points.ToString(); 
    }
}
