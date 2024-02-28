using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class Player : MonoBehaviour
{

    public GameObject prefabBoomEffect;
    public GameObject prefabBullet;
    public float speed = 3;

    public float maxHp = 20;

    Vector3 input;
    bool dead = false;

    public float hp;
    MeshRenderer render;

    Weapon weapon;
    public int weaponAvailable;

    Vector3 targetPosition = new Vector3(0, 1, 0);
    GameMode gameMode;

    void Start()
    {
        weaponAvailable = 0;
        hp = maxHp;
        render = GetComponent<MeshRenderer>();
        render.material.color = Random.ColorHSV();
        weapon = GetComponent<Weapon>();

        gameMode = GameObject.Find("GameMode").GetComponent<GameMode>();
        gameMode.OnPlayerHpChange(hp);
    }



    //void Update()
    //{
    //    // Check if the left mouse button was clicked
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        // Create a ray from the mouse cursor on screen in the direction of the camera
    //        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //        RaycastHit hit;

    //        // Perform the raycast and if it hits something on the ground layer, move the player to the hit point
    //        if (Physics.Raycast(ray, out hit))
    //        {
    //            targetPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);
    //        }
    //    }

    //    bool fireKeyDown = Input.GetKeyDown(KeyCode.J);
    //    bool fireKeyPressed = Input.GetKey(KeyCode.J);
    //    bool changeWeapon = Input.GetKeyDown(KeyCode.Q);

    //    if (!dead)
    //    {
    //        Move();
    //        weapon.Fire(fireKeyDown, fireKeyPressed, render.material.color);

    //        if (changeWeapon)
    //        {
    //            ChangeWeapon();
    //        }
    //    }
    //}

    //void Move()
    //{
    //    // Move towards the target position
    //    Vector3 direction = targetPosition - transform.position;
    //    if (direction.magnitude > 0.1f)
    //    {
    //        direction = direction.normalized;
    //        transform.position += direction * speed * Time.deltaTime;
    //        transform.forward = direction;
    //    }

    //    Vector3 temp = transform.position;
    //    const float BORDER = 20;
    //    if (temp.z > BORDER) { temp.z = BORDER; }
    //    if (temp.z < -BORDER) { temp.z = -BORDER; }
    //    if (temp.x > BORDER) { temp.x = BORDER; }
    //    if (temp.x < -BORDER) { temp.x = -BORDER; }
    //    transform.position = temp;
    //}


    //void Update()
    //{
    //    float moveInput = Input.GetAxis("Vertical");
    //    float rotateInput = Input.GetAxis("Horizontal");

    //    bool fireKeyDown = Input.GetKeyDown(KeyCode.J);
    //    bool fireKeyPressed = Input.GetKey(KeyCode.J);
    //    bool changeWeapon = Input.GetKeyDown(KeyCode.Q);

    //    if (!dead)
    //    {
    //        Move(moveInput, rotateInput);
    //        weapon.Fire(fireKeyDown, fireKeyPressed, render.material.color);

    //        if (changeWeapon)
    //        {
    //            ChangeWeapon();
    //        }
    //    }
    //}

    //void Move(float moveInput, float rotateInput)
    //{
    //    // Move forward or backward
    //    Vector3 move = transform.forward * moveInput * speed * Time.deltaTime;
    //    transform.position += move;

    //    // Rotate left or right
    //    float rotate = rotateInput * speed * 50 * Time.deltaTime;
    //    transform.Rotate(0, rotate, 0);

    //    Vector3 temp = transform.position;
    //    const float BORDER = 20;
    //    if (temp.z > BORDER) { temp.z = BORDER; }
    //    if (temp.z < -BORDER) { temp.z = -BORDER; }
    //    if (temp.x > BORDER) { temp.x = BORDER; }
    //    if (temp.x < -BORDER) { temp.x = -BORDER; }
    //    transform.position = temp;
    //}

    void Update()
    {
        input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));


        bool fireKeyDown = Input.GetKeyDown(KeyCode.J);
        bool fireKeyPressed = Input.GetKey(KeyCode.J);
        bool changeWeapon = Input.GetKeyDown(KeyCode.Q);

        if (!dead)
        {
            Move();
            weapon.Fire(fireKeyDown, fireKeyPressed, render.material.color);

            if (changeWeapon)
            {
                ChangeWeapon();
            }
        }
    }

    void Move()
    {
        input = input.normalized;
        transform.position += input * speed * Time.deltaTime;
        if (input.magnitude > 0.1f)
        {
            transform.forward = input;
        }

        Vector3 temp = transform.position;
        const float BORDER = 20;
        if (temp.z > BORDER) { temp.z = BORDER; }
        if (temp.z < -BORDER) { temp.z = -BORDER; }
        if (temp.x > BORDER) { temp.x = BORDER; }
        if (temp.x < -BORDER) { temp.x = -BORDER; }
        transform.position = temp;
    }

    private void ChangeWeapon()
    {
        int w = weapon.Change(weaponAvailable);
        gameMode.SetWeaponText(w);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyBullet"))
        {

            if (dead)
            {
                return;
            }
            --hp;
            gameMode.OnPlayerHpChange(hp);
            // render.material.color = Color.Lerp(Color.white, Color.red, 1 - hp / maxHp);
            if (hp <= 0)
            {
                dead = true;
                Destroy(gameObject);
            }
        }
        else if (other.CompareTag("Ally") || other.CompareTag("Enemy"))
        {
            Vector3 collisionNormal = other.transform.position - transform.position;
            other.transform.position += collisionNormal.normalized / 10f;
            transform.position -= collisionNormal.normalized / 10f;
        }
        else if (other.CompareTag("Face"))
        {
            // find the parent of the face
            Transform parent = other.transform.parent;
            Vector3 collisionNormal = parent.transform.position - transform.position;
            parent.transform.position += collisionNormal.normalized / 10f;
            transform.position -= collisionNormal.normalized / 10f;
        }
    }


}
