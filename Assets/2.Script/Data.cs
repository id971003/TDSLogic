using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lib
{
    public static class Data
    {
        // Truck & Hero
        public static Vector2 TruckMoveVector => new Vector2(0.3f * Time.deltaTime, 0); //Ʈ�� �̵��ӵ�
        public static readonly Vector3 BoxStartPositionY = new Vector3(0, 0.83f, 0); //���� ������ġ
        public static readonly Vector3 BoxGapY = new Vector3(0, 1.5f, 0); // ���ڰ���
        public static readonly Vector3 HeroGapY = new Vector3(0, 1.4f, 0); //���� ����
        public static Vector2 backgroundSpeed => new Vector2(0.2f * Time.deltaTime, 0); //��� �ӵ�
        public static float WheelRotateSpeed => -130f * Time.deltaTime; // ���� ȸ�� �ӵ�
        public const float TruckHeroHp = 50f; // Ʈ���� ������ ü��
        public const int TruckStopMonsterCount = 3; //Ʈ���� ���ߴ� ���� ����
        public static readonly WaitForSeconds TruckCheckCount = Yielders.WaitForSeconds(0.2f); //Ʈ�� ���� ���� äũ �ð�

        public static readonly WaitForSeconds TruckHeroShotDelay = Yielders.WaitForSeconds(0.3f); //���� ���ݼӵ�
        public const float BulletLifeTime = 0.5f; //�Ѿ� �����ð�
        public const float BulletDamage = 5f; //�Ѿ˵�����
        public const float BulletSpeed = 30f; //�Ѿ˼ӵ�

        // Monster
        public static readonly Vector3 MonsterSpawnOffset = new Vector3(10, 1, 0); //���� ���� ��
        public static readonly WaitForSeconds MonsterSpawnTime = Yielders.WaitForSeconds(1f); // ���� ���� �ð�
        public const float EnemySpeed = 1.5f; //���� �̵��ӵ�
        public const float MonsterMaxHp = 15f; // ���� ü��
        public static readonly Vector2 EenmyJumpVector = new Vector2(-200, 800); //���� ����
        public static readonly Vector2 checkLeftUpVector = new Vector2(-0.5f, 1);
        public const float checkMonsterDistanceFront = 0.5f; // ���� ���� üũ �Ÿ� -1,0
        public const float checkMonstserDistanceLeftUp = 3f; //  ���� ���� üũ �Ÿ� -0.5f,1
        public const float checkMonstserDistanceUp = 5f; //  ���� ���� üũ �Ÿ� 0,1
        public static readonly WaitForSeconds MonsterJumpDelay = Yielders.WaitForSeconds(1); //���� ���� ������
        public const float MonsterAttackDistance = 1.8f; //���� ���ݰŸ�
        public const float MonsterDmg = 1f; //���͵�����

        public const float CameraSpeed = 0.01f; // ī�޶� �ӵ�
        public static readonly Vector3 CameraOffSet = new Vector3(2f, 2.24f, -10f); //ī�޶� ������

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