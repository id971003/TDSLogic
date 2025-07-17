using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lib
{
    public static class Data
    {
        // Truck & Hero
        public static Vector2 TruckMoveVector => new Vector2(0.3f * Time.deltaTime, 0); //트럭 이동속도
        public static readonly Vector3 BoxStartPositionY = new Vector3(0, 0.83f, 0); //상자 시작위치
        public static readonly Vector3 BoxGapY = new Vector3(0, 1.5f, 0); // 상자간격
        public static readonly Vector3 HeroGapY = new Vector3(0, 1.4f, 0); //영웅 간격
        public static Vector2 backgroundSpeed => new Vector2(0.2f * Time.deltaTime, 0); //배경 속도
        public static float WheelRotateSpeed => -130f * Time.deltaTime; // 바퀴 회전 속도
        public const float TruckHeroHp = 50f; // 트럭과 영웅의 체력
        public const int TruckStopMonsterCount = 3; //트럭이 멈추는 몬스터 숫자
        public static readonly WaitForSeconds TruckCheckCount = Yielders.WaitForSeconds(0.2f); //트럭 정지 몬스터 채크 시간

        public static readonly WaitForSeconds TruckHeroShotDelay = Yielders.WaitForSeconds(0.3f); //영웅 공격속도
        public const float BulletLifeTime = 0.5f; //총알 생존시간
        public const float BulletDamage = 5f; //총알데미지
        public const float BulletSpeed = 30f; //총알속도

        // Monster
        public static readonly Vector3 MonsterSpawnOffset = new Vector3(10, 1, 0); //몬스터 스폰 갭
        public static readonly WaitForSeconds MonsterSpawnTime = Yielders.WaitForSeconds(1f); // 몬스터 스폰 시간
        public const float EnemySpeed = 1.5f; //몬스터 이동속도
        public const float MonsterMaxHp = 15f; // 몬스터 체력
        public static readonly Vector2 EenmyJumpVector = new Vector2(-200, 800); //몬스터 점프
        public static readonly Vector2 checkLeftUpVector = new Vector2(-0.5f, 1);
        public const float checkMonsterDistanceFront = 0.5f; // 몬스터 점프 체크 거리 -1,0
        public const float checkMonstserDistanceLeftUp = 3f; //  몬스터 점프 체크 거리 -0.5f,1
        public const float checkMonstserDistanceUp = 5f; //  몬스터 점프 체크 거리 0,1
        public static readonly WaitForSeconds MonsterJumpDelay = Yielders.WaitForSeconds(1); //몬스터 점프 딜레이
        public const float MonsterAttackDistance = 1.8f; //몬스터 공격거리
        public const float MonsterDmg = 1f; //몬스터데미지

        public const float CameraSpeed = 0.01f; // 카메라 속도
        public static readonly Vector3 CameraOffSet = new Vector3(2f, 2.24f, -10f); //카메라 오프셋

        // Layer and Tag
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