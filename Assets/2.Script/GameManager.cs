using Lib;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;


public class GameManager : Singletone<GameManager>
{
    [SerializeField] private Truck truck;
    

    Coroutine rootSpawn;


    
    
    List<HitObject> monsters = new List<HitObject>();



    [SerializeField] private GameObject[] PrefabMonster;

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
        int MonsterIndex = Random.Range(0, PrefabMonster.Length);
        int LayerIndex = Random.Range(0, Data.LayerName.Length);
        
        GameObject monster = Poolable.TryGetPoolable(PrefabMonster[MonsterIndex]);   //TODO  Pooling
        var Monster = monster.GetComponent<Monster>();
        monsters.Add(Monster);
        monster.transform.position = truck.transform.position + Data.MonsterSpawnOffset;
        Monster.SetUp(LayerIndex,truck.transform);   
    }

    public void RemoveMonster(Monster monster)
    {
        if (monsters.Contains(monster))
        {
            monsters.Remove(monster);
            Poolable.TryPool(monster.gameObject);
        }
    }






    public HitObject GetCloseBox(Transform monster)
    {
        return truck.ReturnCurrentHitPoj(monster);
    }

    public void Testcode()
    {
        
    }





}

