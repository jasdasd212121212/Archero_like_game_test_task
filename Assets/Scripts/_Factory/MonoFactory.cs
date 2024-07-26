using UnityEngine;
using Zenject;

public class MonoFactory<T> where T : Object
{
    private T _prefab;
    private Transform _parent;
    private DiContainer _container;

    public MonoFactory() { }

    public MonoFactory(DiContainer container) 
    { 
        _container = container;
    }

    public MonoFactory(T prefab)
    {
        _prefab = prefab;
    }

    public MonoFactory(T prefab, DiContainer container) : this(container)
    {
        _prefab = prefab;
    }

    public MonoFactory(T prefab, Transform parent) : this(prefab)
    {
        _parent = parent;
    }

    public MonoFactory(T prefab, Transform parent, DiContainer container) : this(prefab)
    {
        _parent = parent;
        _container = container;
    }

    public T CreateWithParent(T prefab)
    {
        if (_parent == null)
        {
            Debug.LogError($"Critical error -> Can`t create because {nameof(_parent)} is null");
            return default;
        }

        if (_container == null)
        {
            return GameObject.Instantiate(prefab, _parent);
        }
        else
        {
            return _container.InstantiatePrefab(prefab, _parent).GetComponent<T>();
        }
    }

    public T CreateWithParent()
    {
        return CreateWithParent(_prefab);
    }

    public T CreateWithoutParent(T prefab, Vector3 position, Quaternion quaternion)
    {
        if (_container == null)
        {
            return GameObject.Instantiate(prefab, position, quaternion);
        }
        else
        {
            return _container.InstantiatePrefab(prefab, position, quaternion, null).GetComponent<T>();
        }
    }

    public T CreateWithoutParent(Vector3 position, Quaternion quaternion)
    {
        return CreateWithoutParent(_prefab, position, quaternion);
    }
}