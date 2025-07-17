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



    ResourceManager _resourceManager;

    List<HitObject> Monsters = new List<HitObject>();
    



    
    [SerializeField] private BoxCollider2D boxcollider; //적 못지나가게 막던 Truck콜라이더


    public bool B_GameStart => b_GameStart; // Game Start
    [SerializeField] private bool b_GameStart;

    public Action gameEnd;

    public bool B_Move=> b_Move; // Truck Moveable
    [SerializeField] private bool b_Move;



    

    public void Start()
    {
        _resourceManager= ResourceManager.Instance;
        GameStart();
    }
    #region StartAndEnd
    void GameStart()
    {
        b_GameStart = true;
        b_Move = true;
        truck.RootStart();
        rootSpawn = StartCoroutine(RootSpawnMonster());
        rootCheckCloseMonster = StartCoroutine(CheckCloseMonsterRoot());
    }
    public void GameEnd()
    {
        b_GameStart = false;
        boxcollider.enabled = false;
        gameEnd?.Invoke();
    }
    #endregion

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
    void SpawnMolnster()
    {
        int MonsterIndex = UnityEngine.Random.Range(0, _resourceManager.PrefabMonster.Length);
        int LayerIndex = UnityEngine.Random.Range(0, Data.LayerName.Length);
        
        GameObject monster = Poolable.TryGetPoolable(_resourceManager.PrefabMonster[MonsterIndex]);   //TODO  Pooling
        var Monster = monster.GetComponent<Monster>();
        Monsters.Add(Monster);
        monster.transform.position = truck.transform.position + Data.MonsterSpawnOffset;
        Monster.SetUp(LayerIndex,truck.transform, _resourceManager.material);   
    }

    public void RemoveMonster(Monster monster)
    {
        if(Monsters.RemoveBySwap(monster))
        {
            Poolable.TryPool(monster.gameObject);
        }
        
    }
    #endregion

    #region Return_HitObj
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

    #endregion
    





}

