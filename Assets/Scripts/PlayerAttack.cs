using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform sword;
    public Camera camera;
    private Transform tr;
    private Vector3 mousePos;

    void Start()
    {
        tr = GetComponent<Transform>();
    }
    void Update()
    {
        if (Time.timeScale != 0)
            ChangeSword();
    }

    void ChangeSword()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 diff = mousePos - tr.position;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        sword.SetPositionAndRotation(sword.position, Quaternion.Euler(0f, 0f, rot_z - 90f));
        sword.position = Vector2.MoveTowards(sword.position, tr.position - (tr.position - mousePos).normalized * 5, Time.deltaTime * 50);
    }
}
