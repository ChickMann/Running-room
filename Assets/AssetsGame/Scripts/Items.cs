using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static GameManager;

public enum ItemType { None, SpeedUp = 5, SpeedDown = -5, Money = 10, star = 0 }
public class Items : MonoBehaviour
{
    private MeshRenderer m_Renderer;
    public ItemType Type;
    private SpawnManager spawnManager;
    private void Awake()
    {
        m_Renderer = GetComponent<MeshRenderer>();
    }
    private void Update()
    {
        if (Type == ItemType.star)
        {
            Color color = doorScript_Instance.color[Random.Range(0, doorScript_Instance.color.Length - 1)].color;
            m_Renderer.material.color = color;
        }
    }
    private void Start()
    {
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
    }
    public void objectPolling()
    {
        transform.position = spawnManager.PosSpawn();
    }

}
