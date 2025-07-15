using System.Collections.Generic;
using TaskbarHero;
using Lib;
using UnityEngine;
namespace TaskbarHero
{
    public class PoolManager : Singletone<PoolManager>
    {
        

        Dictionary<Poolable, Pool> _pool = new Dictionary<Poolable, Pool>();
        Dictionary<string, Pool> _poolByName = new Dictionary<string, Pool>();
        Transform _root;

        private void Awake()
        {
            Init();
        }

        

        public void Init()
        {
            
            gameObject.name = "PoolManager";
            _root = this.transform;
            

            DontDestroyOnLoad(this);
        }

        public void CreatePool(Poolable original, int count = 5)
        {
            Pool pool = new Pool();
            pool.Init(original, count);
            pool.Root.parent = _root;

            _pool.Add(original, pool);
        }

        public void CreatePool(Poolable original, string poolName, int count = 5)
        {
            Pool pool = new Pool();
            pool.Init(original, count, poolName);
            pool.Root.parent = _root;

            _poolByName.Add(poolName, pool);
        }


        public void CreatePool(Poolable original, Transform parent, int count = 5)
        {
            Pool pool = new Pool();
            pool.Init(original, count);
            pool.Root.parent = parent;

            _pool.Add(original, pool);
        }

        public void CreatePool(Poolable original, Transform parent, string poolName, int count = 5)
        {
            Pool pool = new Pool();
            pool.Init(original, count, poolName);
            pool.Root.parent = parent;

            _poolByName.Add(poolName, pool);
        }

        // 런타임시 리소스 리로딩을 위해 pool을 재구성하는 함수
        public void ResetPool(Poolable original, int count = 5)
        {
            Pool pool;
            if (_pool.ContainsKey(original) == false)
                return;

            pool = _pool[original];
            var root = pool.Root;
            var childs = root.GetComponentsInChildren<Poolable>();
            foreach (var p in childs)
            {
                p.shouldDestroy = true;
            }

            pool.ResetPool(original);
        }

        public Poolable Pop(Poolable original, string poolName = null)
        {
            // FIXME: 현재 original.name에서 가비지 생성중
            Pool pool;

            if (poolName != null)
            {
                if (_poolByName.ContainsKey(poolName) == false)
                {
                    CreatePool(original, poolName);
                }

                pool = _poolByName[poolName];
            }
            else
            {
                if (_pool.ContainsKey(original) == false)
                {
                    CreatePool(original);
                }

                pool = _pool[original];
            }
            return pool.Pop();
        }

        public Poolable Pop(Poolable original, Transform parent, string poolName = null)
        {
            // FIXME: 현재 original.name에서 가비지 생성중
            Pool pool;

            if (poolName != null)
            {

                if (_poolByName.ContainsKey(poolName) == false)
                {
                    if (parent == null)
                    {
                        CreatePool(original, poolName);
                    }
                    else
                    {
                        CreatePool(original, parent, poolName);
                    }
                }

                pool = _poolByName[poolName];
            }
            else
            {
                if (_pool.ContainsKey(original) == false)
                {
                    if (parent == null)
                    {
                        CreatePool(original);
                    }
                    else
                    {
                        CreatePool(original, parent);
                    }
                }

                pool = _pool[original];
            }


            return pool.Pop();
        }


        public void Clear()
        {
            foreach (Transform child in _root)
                GameObject.Destroy(child.gameObject);

            _pool.Clear();
        }
    }
}