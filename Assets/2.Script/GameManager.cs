using Lib;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;


public class GameManager : Singletone<GameManager>
{
    [SerializeField] private Truck truck;
    Coroutine rootSpawn;



    [SerializeField] private GameObject[] go_Monster;

    public bool B_Move=> b_Move; // Truck Moveable
    [SerializeField] private bool b_Move;
    public void Start()
    {
        b_Move = true;
        truck.RootStart();
        rootSpawn = StartCoroutine(RootSpawnMonster());

    }
    public void TURCKMOVE(bool a)
    {
        b_Move = a;
    }

    public IEnumerator RootSpawnMonster()
    {
        while (true)
        {
            SpawnMolnster();
            yield return Data.MonsterSpawnTime;
        }
    }
    public void SpawnMolnster()
    {
        int MonsterIndex = Random.Range(0, go_Monster.Length);
        int LayerIndex = Random.Range(0, Data.LayerName.Length);
        
        GameObject monster = Poolable.TryGetPoolable(go_Monster[MonsterIndex]);   //TODO  Pooling
        var Monster = monster.GetComponent<Monster>();
        Monster.SetUp(LayerIndex);
        monster.transform.position = truck.transform.position + Data.MonsterSpawnOffset;
    }


}

