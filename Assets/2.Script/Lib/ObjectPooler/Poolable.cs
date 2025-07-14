using TaskbarHero;
using Unity.VisualScripting;
using UnityEngine;
using Lib;
public class Poolable : MonoBehaviour
{
    public bool IsUsing { get; set; }

    public Pool pool;
    public bool shouldDestroy { get; set; }


    protected virtual void Repool()
    {
        if (shouldDestroy)
        {
            Object.Destroy(gameObject);
            return;
        }

        pool.Push(this);
    }


    public virtual void OnPush()
    {
        gameObject.SetActive(false);
    }

    public virtual void OnPop()
    {
        gameObject.SetActive(true);

    }

    public static void TryPool(GameObject go)
    {
        if (go == null)
        {
            return;
        }

        Poolable poolable = go.GetComponentNoGarbage<Poolable>();
        if (poolable != null && poolable.pool != null)
        {
            poolable.Repool();
            return;
        }
        Object.Destroy(go);
    }

    public static GameObject TryGetPoolable(GameObject original, Transform parent = null, string poolName = null)
    {
        // 처음 풀링 오브젝트 생성시, poolable이 pool을 가지고있지 않음.
        // PoolManager를 통해 pool만드는 과정을 거침
        Poolable poolable = original.GetComponentNoGarbage<Poolable>();
        if (poolable != null)
        {
            // 오브젝트 이름이 아닌 특정 poolName으로 만드려고 하는 경우
            if (poolName != null)
            {
                if (parent == null)
                {
                    return PoolManager.Instance.Pop(poolable, poolName).gameObject;
                }
                else
                {
                    return PoolManager.Instance.Pop(poolable, parent, poolName).gameObject;
                }
            }

            if (parent == null)
            {
                return PoolManager.Instance.Pop(poolable).gameObject;
            }
            else
            {
                return PoolManager.Instance.Pop(poolable, parent).gameObject;
            }
        }
        else
        {
            GameObject go = Object.Instantiate(original, parent);
            go.name = original.name;
            return go;
        }


    }
    public static void InitPoolabe(GameObject original, Transform parent = null)
    {
        TryPool(TryGetPoolable(original, parent));
    }
}

