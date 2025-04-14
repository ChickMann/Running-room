using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEngine.Debug;
using static GameManager;

[RequireComponent(typeof(BoxCollider), typeof(PlayerContronller))]
[RequireComponent(typeof(Rigidbody), typeof(Animator))]
public class PlayerContronller : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private ParticleSystem explore;
    [SerializeField] private AudioClip aduioClipEnemy;
    [SerializeField] private AudioClip aduioClipMoney;
    [SerializeField] private AudioClip aduioClipBoost;
    [SerializeField] private AudioSource audioEffect;
    [SerializeField] private GameObject marker;

    private ItemType currentItem = ItemType.None;
    private Rigidbody rb;
    private Animator anim;
    private GameObject enemy;
    private float speedStart;
    private float inputHoz;



    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        speedStart = speed;
        inputHoz = 1;

    }

    void Update()
    {

        if (GameManager_Instance.isGameOver)
        {
            inputHoz = 0;
            anim.SetInteger("Animation_int", 0);
        }
    }
    private void FixedUpdate()
    {

        if (GameManager_Instance.hasStart)
        {

            marker.SetActive(true);
            anim.SetBool("Start", true);
            PlayerMove();
        }

        FlipDirRotation();

    }



    #region Collision & Trigger
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            audioEffect.Stop();
            audioEffect.PlayOneShot(aduioClipEnemy);
            explore.transform.position = transform.position + Vector3.up;
            explore.Play();
            anim.SetBool("Death_b", true);
            anim.SetInteger("DeathType_int", 1);
            GameManager_Instance.GameOVer();
            enemy = collision.gameObject;
            StartCoroutine(GameManager_Instance.CoolDown(() => GameManager_Instance.ShowGameBoard(), 3f));
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            Items item = other.gameObject.GetComponent<Items>();
            currentItem = item.Type;
            switch (currentItem)
            {
                case ItemType.SpeedUp:
                    speed += (int)currentItem;
                    if (!audioEffect.isPlaying)
                    {
                        audioEffect.PlayOneShot(aduioClipBoost);
                    }
                    break;

                case ItemType.Money:
                    GameManager_Instance.IncreaseScore((int)currentItem);
                    audioEffect.PlayOneShot(aduioClipMoney);
                    break;

                case ItemType.star:
                    doorScript_Instance.RandomDoorManyTimes();
                    Destroy(other.gameObject);
                    spawnManager_Instance.SpawnStarTime(10);
                    break;
            }


            GameManager_Instance.SpawnItems();
            item.objectPolling();

        }
    }
    #endregion
    #region Button Funcions

    public void ButtonMove()
    {
        if (inputHoz == 0)
        {
            Log(transform.localEulerAngles.y);
            if (transform.localEulerAngles.y == 90)
            {
                inputHoz = 1;
            }
            else if (transform.localEulerAngles.y == 270)
            {
                inputHoz = -1;
            }
        }
        else if (inputHoz == 1)
        {
            inputHoz = -1;
        }
        else
        {
            inputHoz = 1;
        }


    }

    #endregion
    #region Moment Actions
    void PlayerMove()
    {
        //inputHoz = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector3(speed * inputHoz, rb.linearVelocity.y, 0);
        anim.SetFloat("Speed_f", Mathf.Abs(rb.linearVelocity.x));
        anim.SetFloat("StateRun", speed == speedStart ? 1.5f : speed > speedStart ? 2f : 0.5f);
    }


    public void FlipDirRotation()
    {
        float inputX;
        float inputXCompare;
        float rotationY;
        if (enemy != null)
        {
            inputX = enemy.transform.position.x;
            inputXCompare = transform.position.x;

        }
        else
        {
            inputX = rb.linearVelocity.x;
            inputXCompare = 0; ;
        }
        rotationY = inputX == inputXCompare ?
        transform.localEulerAngles.y : inputX > inputXCompare ? 90f : -90f;
        transform.localEulerAngles = new Vector3(0, rotationY, 0);

    }

    #endregion
}
