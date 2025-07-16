using Lib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;


public class GameManager : Singletone<GameManager>
{
    [SerializeField] private Truck truck;
    

    Coroutine rootSpawn;
    Coroutine rootCheckCloseMonster;





    List<HitObject> Monsters = new List<HitObject>();
    [SerializeField] List<HitObject> HitMonster = new List<HitObject>(); //때리고있는적



    [SerializeField] private GameObject[] PrefabMonster;



    public bool B_GameStart => b_GameStart; // Game Start
    [SerializeField] private bool b_GameStart;

    public Action gameEnd;

    public bool B_Move=> b_Move; // Truck Moveable
    [SerializeField] private bool b_Move;



    [SerializeField] Material material;

    public void Start()
    {
        b_GameStart = true;
        b_Move = true;
        truck.RootStart();
        rootSpawn = StartCoroutine(RootSpawnMonster());
        rootCheckCloseMonster = StartCoroutine(CheckCloseMonsterRoot());
    }
    void GameStart()
    {
        b_GameStart = true;
        b_Move = true;
        truck.RootStart();
        rootSpawn = StartCoroutine(RootSpawnMonster());
    }
    public void GameEnd()
    {
        b_GameStart = false;
        gameEnd?.Invoke();
    }

    #region Truck controll

    IEnumerator CheckCloseMonsterRoot()
    {
        while(b_GameStart)
        {
            yield return Data.TruckCheckCount;
            int count = 0;
            bool over = false;
            foreach (var monster in Monsters)
            {
                if (monster.B_Alive)
                {
                    float distance = monster.transform.position.x - truck.transform.position.x;
                    if (distance < Data.MonsterAttackDistance)
                    {
                        count++;
                        if(count>= Data.TruckStopMonsterCount)
                        {
                            TURCKMOVE(false);
                            over = true;
                            break;
                        }

                    }
                }
            }
            TURCKMOVE(!over);
        }

    }

    public void TURCKMOVE(bool a)
    {
        b_Move = a;
    }
    #endregion


    #region Monster
    public IEnumerator RootSpawnMonster()
    {
        while (b_GameStart)
        {
            SpawnMolnster();
            yield return Data.MonsterSpawnTime;
        }
    }
    public void SpawnMolnster()
    {
        int MonsterIndex = UnityEngine.Random.Range(0, PrefabMonster.Length);
        int LayerIndex = UnityEngine.Random.Range(0, Data.LayerName.Length);
        
        GameObject monster = Poolable.TryGetPoolable(PrefabMonster[MonsterIndex]);   //TODO  Pooling
        var Monster = monster.GetComponent<Monster>();
        Monsters.Add(Monster);
        monster.transform.position = truck.transform.position + Data.MonsterSpawnOffset;
        Monster.SetUp(LayerIndex,truck.transform, material);   
    }

    public void RemoveMonster(Monster monster)
    {
        if (Monsters.Contains(monster))
        {
            Monsters.Remove(monster);
            Poolable.TryPool(monster.gameObject);
        }
    }
    #endregion






    public HitObject GetCloseBox(Transform monster)
    {
        return truck.ReturnCurrentHitPoj(monster);
    }
    public HitObject GetCloseMonster()
    {
        HitObject closestMonster = null;
        float closestDistance = float.MaxValue;
        foreach (var monster in Monsters)
        {
            if (monster.B_Alive)
            {
                float distance = Vector3.Distance(truck.transform.position, monster.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestMonster = monster;
                }
            }
        }
        return closestMonster;

    }

    public void Testcode()
    {
        
    }





}

