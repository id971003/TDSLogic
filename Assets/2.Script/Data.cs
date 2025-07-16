using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lib
{
    public static class Data
    {

        public static Vector2 backgroundSpeed = new Vector2(0.2f * Time.deltaTime, 0);
        public static Vector2 TruckMoveVector = new Vector2(0.3f * Time.deltaTime, 0);
        public static float WheelRotateSpeed = -130f* Time.deltaTime; // Speed of wheel rotation in degrees per second

        public static Vector3 BoxStartPositionY =new Vector3(0, 0.83f,0);
        public static Vector3 BoxGapY = new Vector3(0, 1.5f, 0);
        public static Vector3 HeroGapY = new Vector3(0, 1.4f);// 1.409f, 0);


        //Truck & Hero
        public static float TruckHeroHp = 100;

        public static float BulletSpeed = 10;
        public static float BulletLifeTime = 2f; // Time before the bullet is destroyed
        public static float BulletDamage = 10; // Damage dealt by the bullet




        //Monster
        public static Vector3 MonsterSpawnOffset = new Vector3(10, 1, 0); // Center
        public static WaitForSeconds MonsterSpawnTime = Yielders.WaitForSeconds(1f); // Time between monster spawns 
        public static float EnemySpeed_Max = 1;
        public static float EnemySpeed_Min = 1;
        public static Vector2 EenmyJumpVector = new Vector2(-400, 800);
        public static float checkDistance = 0.4f; // Distance to check for other monsters
        public static float checkDistance2 = 5; // Distance to check for other monsters
        public static WaitForSeconds MonsterJumpDelay=Yielders.WaitForSeconds(1); // Delay before jumping again
        public static float MonsterAttackDistance = 1.6f;
        public static float MonsterDmg=3;



        public static float CameraSpeed = 0.01f; // Speed of the camera movement
        public static Vector3 CameraOffSet = new Vector3(2f, 2.24f, -10f); // Offset of the camera from the target

        
        

        public static string[] LayerName = new string[]
        {
            "EnemyLayer_1",
            "EnemyLayer_2",
            "EnemyLayer_3"
        };
    }
}