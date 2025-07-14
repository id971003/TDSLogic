using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lib;


public class GameManager : MonoBehaviour
{
    [SerializeField] private Truck truck;




    Coroutine rootSpawn;



    [SerializeField] private GameObject[] go_Monster;


    public void Start()
    {
        truck.RootStart();
        rootSpawn = StartCoroutine(RootSpawnMonster());

    }

    public IEnumerator RootSpawnMonster()
    {
        while (true)
        {


            int MonsterIndex = Random.Range(0, go_Monster.Length);
            int LayerIndex = Random.Range(0, Data.LayerName.Length);

            GameObject monster = Instantiate(go_Monster[MonsterIndex]);  //TODO  Pooling
            var Monster = monster.GetComponent<Monster>();
            Monster.SetUp(LayerIndex);
            monster.transform.position = truck.transform.position + Data.MonsterSpawnOffset[0];




            yield return Data.MonsterSpawnTime;
        }
    }


}

