using System;
using System.Collections.Generic;
using Test_Project.Abstractions;
using UnityEngine;

namespace Test_Project
{
    public sealed class SimpleObjectPull<T> where T : MonoBehaviour, IPullable
    {
        private readonly int _beginSpawnAmount = 1;
        private readonly int _autoSpawnAmount = 1;
        private readonly Transform _container;
        private readonly T _prefab;
        private readonly List<T> _objects = new List<T>();
        private readonly IInstigator _instigator;

        public event Action<T> NewObjectJustCreated; 

        
        public SimpleObjectPull(IInstigator instigator,Transform container, T prefab)
        {
            _instigator = instigator;
            _container = container;
            _prefab = prefab;
        }
        public SimpleObjectPull(IInstigator instigator,Transform container, T prefab,int beginSpawnAmount, int autoSpawnAmount)
        {
            if (_beginSpawnAmount <= 0)
            {
                Debug.LogError("Begin spawn amount cant be negative or zero");
            }
            if (_autoSpawnAmount <= 0)
            {
                Debug.LogError("Auto spawn amount cant be negative or zero");
            }
            _instigator = instigator;
            _beginSpawnAmount = beginSpawnAmount;
            _autoSpawnAmount = autoSpawnAmount;
            _container = container;
            _prefab = prefab;
        }
        
        public void SpawnBeginObjects()=> CreateObjects(_beginSpawnAmount);
        

        public T GetObject()
        {
            if (HasAvailableObject() == false)
            {
                CreateObjects(_autoSpawnAmount);
            }
            return GetObjectFromPull();
        }

        private void CreateObjects(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                var obj = _instigator.Instantiate(_prefab);
                AddObjectToPull(obj);
                OnNewObjectJustCreated(obj);
            }
        }

        public void AddObjectToPull(T obj)
        {
            obj.Deactivate();
            _objects.Add(obj);
            obj.transform.SetParent(_container);
        }

        private bool HasAvailableObject()
        {
            foreach (var obj in _objects)
            {
                if (obj.IsAvailable())
                {
                    return true;
                }
            }

            return false;
        }

        private T GetObjectFromPull()
        {
            foreach (var obj in _objects)
            {
                if (obj.IsAvailable())
                {
                    obj.Activate();
                    _objects.Remove(obj);
                    return obj;
                }
            }

            Debug.LogWarning("The pulled object is null");
            return null;
        }
        private void OnNewObjectJustCreated(T obj)
        {
            NewObjectJustCreated?.Invoke(obj);
        }
    }
}