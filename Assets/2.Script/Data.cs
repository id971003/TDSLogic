using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lib
{
    public static class Data
    {

        public static Vector3[] MonsterSpawnOffset=new Vector3[]
        {
            new Vector3(8, -3.61f,0), // Center
            new Vector3(8, -5f,0), // Center
            new Vector3(8, -6,0), // Center
        };

        public static Vector2 TruckMoveVector = new Vector2(0.1f * Time.deltaTime, 0);

        public static float EnemySpeed = 1;


        public static float CameraSpeed = 0.5f; // Speed of the camera movement
        public static Vector3 CameraOffSet = new Vector3(2f, 2.24f, -10f); // Offset of the camera from the target


        public static WaitForSeconds MonsterSpawnTime = Yielders.WaitForSeconds(1f); // Time between monster spawns 

        public static string[] LayerName = new string[]
        {
            "EnemyLayer_1",
            "EnemyLayer_2",
            "EnemyLayer_3"
        };
    }
}