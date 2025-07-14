using System.Collections.Generic;
using UnityEngine;

public class Pool
{
    public Poolable Original { get; private set; }
    public Transform Root { get; private set; }
    private int _count;
    private Stack<Poolable> _poolStack = new Stack<Poolable>();

    public void Init(Poolable original, int count)
    {
        Original = original;
        Root = new GameObject().transform;
        Root.name = $"{original.name}_Root";
        _count = count;

        for (int i = 0; i < _count; i++)
            Push(Create());
    }

    public void Init(Poolable original, int count, string poolName)
    {
        Original = original;
        Root = new GameObject().transform;
        Root.name = $"{poolName}_Root";
        _count = count;

        for (int i = 0; i < _count; i++)
            Push(Create());
    }

    public void ResetPool(Poolable original)
    {
        Original = original;
        while (_poolStack.Count > 0)
        {
            UnityEngine.Object.Destroy(_poolStack.Pop().gameObject);
        }
        _poolStack.Clear();

        for (int i = 0; i < _count; i++)
            Push(Create());
    }

    public void Push(Poolable poolable)
    {
        if (poolable == null)
            return;

        poolable.transform.SetParent(Root);
        poolable.IsUsing = false;
        poolable.OnPush();
        _poolStack.Push(poolable);
    }

    public Poolable Pop()
    {
        Poolable poolable;

        if (_poolStack.Count > 0)
            poolable = _poolStack.Pop();
        else
            poolable = Create();

        poolable.pool = this;
        poolable.IsUsing = true;
        poolable.OnPop();

        return poolable;
    }

    private Poolable Create()
    {
        Poolable go = UnityEngine.Object.Instantiate<Poolable>(Original, Root);
        go.name = Original.name;
        return go;
    }

    private Poolable Create(string poolName)
    {
        Poolable go = UnityEngine.Object.Instantiate<Poolable>(Original, Root);
        go.name = poolName;
        return go;
    }
}

