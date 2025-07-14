using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using lib;


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
                yield return Data.MonsterSpawnTime;
                int randomIndex = Random.Range(0, go_Monster.Length);
                GameObject monster = Instantiate(go_Monster[randomIndex]);  //TODO  Pooling
                monster.transform.position = new Vector3(truck.transform.position.x + Data.SpawnDistance, 0, 0);
            }
        }


    }

