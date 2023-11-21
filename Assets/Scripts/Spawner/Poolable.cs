using UnityEngine;
using UnityEngine.Pool;

public abstract class Poolable : MonoBehaviour
{
    protected ObjectPool<Poolable> _pool;

    public void SetPool(ObjectPool<Poolable> pool)
    {
        _pool = pool;
    }

    protected virtual void ReturnToPool()
    {
        if (_pool != null)
            _pool.Release(this);
        else
            Destroy(gameObject);
    }
}
