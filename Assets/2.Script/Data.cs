using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace lib
{
    public static class Data
    {

        public static float SpawnDistance = 10; //EnemySpawnDistance

        public static Vector2 TruckMoveVector = new Vector2(0.5f * Time.deltaTime, 0);

        public static float EnemySpeed = 5;


        public static float CameraSpeed = 0.5f; // Speed of the camera movement
        public static Vector3 CameraOffSet = new Vector3(2f, 2.24f, -10f); // Offset of the camera from the target


        public static WaitForSeconds MonsterSpawnTime = Yielders.WaitForSeconds(1f); // Time between monster spawns 

    }
}