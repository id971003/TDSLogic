using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lib
{
    public static class Data
    {
        
        
        
        

        

        // Truck & Hero
        public static Vector2 TruckMoveVector => new Vector2(0.3f * Time.deltaTime, 0);
        public static readonly Vector3 BoxStartPositionY = new Vector3(0, 0.83f, 0);
        public static readonly Vector3 BoxGapY = new Vector3(0, 1.5f, 0);
        public static readonly Vector3 HeroGapY = new Vector3(0, 1.4f, 0);
        public static Vector2 backgroundSpeed => new Vector2(0.2f * Time.deltaTime, 0);
        public static float WheelRotateSpeed => -130f * Time.deltaTime; // Speed of wheel rotation in degrees per second
        public const float TruckHeroHp = 50f;
        public const int TruckStopMonsterCount = 3;
        public static readonly WaitForSeconds TruckCheckCount = Yielders.WaitForSeconds(0.2f); // 트럭 근방 적 체크시간

        public static readonly WaitForSeconds TruckHeroShotDelay = Yielders.WaitForSeconds(0.3f); // Delay between shots
        public const float BulletLifeTime = 0.3f; // Time before the bullet is destroyed
        public const float BulletDamage = 5f; // Damage dealt by the bullet
        public const float BulletSpeed = 30f;

        // Monster
        public static readonly Vector3 MonsterSpawnOffset = new Vector3(10, 1, 0); // Center
        public static readonly WaitForSeconds MonsterSpawnTime = Yielders.WaitForSeconds(1f); // Time between monster spawns 
        public const float EnemySpeed = 1.5f;
        public const float MonsterMaxHp = 15f; // Maximum health of the monster
        public static readonly Vector2 EenmyJumpVector = new Vector2(-200, 800);
        public const float checkMonsterDistanceFront = 0.5f; // Distance to check for other monsters
        public const float checkMonstserDistanceUp = 5f; // Distance to check for other monsters
        public static readonly WaitForSeconds MonsterJumpDelay = Yielders.WaitForSeconds(1); // Delay before jumping again
        public const float MonsterAttackDistance = 1.8f;
        public const float MonsterDmg = 1f;

        public const float CameraSpeed = 0.01f; // Speed of the camera movement
        public static readonly Vector3 CameraOffSet = new Vector3(2f, 2.24f, -10f); // Offset of the camera from the target

        public static readonly string[] LayerName = new string[]
        {
            "EnemyLayer_1",
            "EnemyLayer_2",
            "EnemyLayer_3"
        };
        public static readonly string MonsterTag = "Monster"; // Tag for monsters
        public static readonly int 
            TruckLayerNum= 8; // Tag for monsters

    }
}