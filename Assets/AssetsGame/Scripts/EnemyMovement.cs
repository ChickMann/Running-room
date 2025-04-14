using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static GameManager;

[RequireComponent(typeof(BoxCollider), typeof(EnemyMovement), typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float speedCurrent;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private GameObject marker;
    private Rigidbody rb;
    private Animator anim;
    public float dir;
    private float speedStart;
    private ItemType currentItem = ItemType.None;
    private int state;
    private bool coliderPlayer = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        speedStart = speedCurrent;
        speedCurrent = 0;
        state = Random.Range(1, 3);
        anim.SetBool("Start", true);
    }

    private void Update()
    {

        FlipDirRotation();
        if (GameManager.Instance.isGameOver)
        {
            dir = 0;
            speedCurrent = 0;
            anim.SetInteger("Animation_int", state);
        }
        else if (GameManager.Instance.hasStart)
        {
            speedCurrent = speedStart;
            marker.SetActive(true);
        }




    }
    private void FixedUpdate()
    {

        EnemyMove();
        anim.SetFloat("Speed_f", Mathf.Abs(rb.linearVelocity.x));

    }


    #region Moment Actions
    void FlipDirRotation()
    {
        float inputX;
        float inputXCompare;
        float rotationY;
        if (coliderPlayer)
        {
            inputX = playerContronller_Instance.transform.position.x;
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

    void EnemyMove()
    {

        bool hit = Physics.Raycast(transform.position + (Vector3.up * 2), transform.TransformDirection(Vector3.forward), out RaycastHit hitInfo, Mathf.Infinity, layerMask); ;
        Debug.DrawRay(transform.position + (Vector3.up * 2), transform.TransformDirection(Vector3.forward) * hitInfo.distance, Color.red);
        if (hit)
        {
            if (transform.position.x > playerContronller_Instance.transform.position.x)
            {
                dir = -1;
            }
            else if (transform.position.x < playerContronller_Instance.transform.position.x)
            {
                dir = 1;
            }
        }

        rb.linearVelocity = new Vector3(speedCurrent * dir, rb.linearVelocity.y, 0);
        anim.SetFloat("StateRun", speedCurrent == speedStart ? 1.5f : speedCurrent > speedStart ? 2f : 0.5f);

    }
    #endregion
    #region Collision & Trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            currentItem = other.gameObject.GetComponent<Items>().Type;
            if (currentItem == ItemType.SpeedUp)
            {
                speedStart += (int)ItemType.SpeedUp;
                Destroy(other.gameObject);
                StartCoroutine(GameManager_Instance.CoolDown(() => speedStart -= (int)ItemType.SpeedUp, 5));
            }
            GameManager_Instance.SpawnItems();
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            coliderPlayer = true;
        }
    }


    #endregion
}
