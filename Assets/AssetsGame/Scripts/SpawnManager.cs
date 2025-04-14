using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class SpawnManager : MonoBehaviour
{


    [SerializeField] private GameObject[] itemsPrefabs;
    [SerializeField] private GameObject itemStar;
    [SerializeField] public List<Transform> itemSpawnded = new List<Transform>();
    [SerializeField] private int maxCountSpawn = 15;

    private float posSpawnY;
    private int posSpawnX;

    internal Vector3 PosSpawn()
    {
        int random = Random.Range(0, doorScript_Instance.doorsRight.Count);
        int posX = (int)doorScript_Instance.doorsRight[random].transform.position.x - 6;
        posSpawnY = doorScript_Instance.doorsRight[random].transform.position.y - 0.5f;
        posSpawnX = Random.Range(-posX, posX);
        return new Vector3(posSpawnX, posSpawnY, 0);
    }
    internal void SpawnItem()
    {
        itemSpawnded.RemoveAll((item => item == null));
        while (itemSpawnded.Count < maxCountSpawn)
        {
            int random = Random.Range(0, itemsPrefabs.Length);
            Transform TranItem = Instantiate(itemsPrefabs[random], PosSpawn(), Quaternion.identity).transform;

            foreach (Transform items in itemSpawnded)
            {
                float itemX = transform.position.x;
                float itemsX = transform.position.x;
                float distance = Mathf.Abs(Mathf.Abs(itemX) - Mathf.Abs(itemsX));
                if (transform.position.y == items.transform.position.y && distance <= 3)
                {
                    int posUpdateX = itemX < itemsX ? -3 : 3;
                    transform.position += new Vector3(posUpdateX, 0, 0);
                    break;
                }
            }
            itemSpawnded.Add(TranItem);
        }

    }
    private void SpawnStar()
    {
        Instantiate(itemStar, PosSpawn(), Quaternion.identity);
    }
    internal void SpawnItemNow()
    {
        InvokeRepeating(nameof(SpawnItem), 0, 0.5f);
    }

    internal void SpawnStarTime(int time)
    {
        Invoke(nameof(SpawnStar), time);


    }


}
