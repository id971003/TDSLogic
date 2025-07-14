using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lib
{
    public static class Data
    {

        public static Vector2 TruckMoveVector = new Vector2(0.1f * Time.deltaTime, 0);


        public static Vector3 MonsterSpawnOffset = new Vector3(10, 1, 0); // Center

        



        public static WaitForSeconds MonsterSpawnTime = Yielders.WaitForSeconds(1f); // Time between monster spawns 
        public static float EnemySpeed_Max = 1;
        public static float EnemySpeed_Min = 1;
        public static Vector2 EenmyJumpVector = new Vector2(-400, Random.RandomRange(800f, 850f));
        public static float checkDistance = 0.4f; // Distance to check for other monsters
        public static WaitForSeconds MonsterJumpDelay=Yielders.WaitForSeconds(1); // Delay before jumping again


        public static float CameraSpeed = 0.5f; // Speed of the camera movement
        public static Vector3 CameraOffSet = new Vector3(2f, 2.24f, -10f); // Offset of the camera from the target


        

        public static string[] LayerName = new string[]
        {
            "EnemyLayer_1",
            "EnemyLayer_2",
            //"EnemyLayer_3"
        };
    }
}