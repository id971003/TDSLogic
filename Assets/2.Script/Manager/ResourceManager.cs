using Lib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : Singletone<ResourceManager>
{
    [SerializeField] public GameObject[] PrefabMonster;
    [SerializeField] public GameObject DamageTextures; // ������ �ؽ��ĵ�
    [SerializeField] public Material material; //HitMaterial

}
