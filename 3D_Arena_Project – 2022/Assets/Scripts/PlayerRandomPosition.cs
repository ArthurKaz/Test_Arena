using System;
using System.Collections.Generic;
using Test_Project.Abstractions;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Test_Project
{
    public class PlayerRandomPosition : IRandomPosition
    {
        private List<Vector3> _freePoints;
        private IObjectContainer<IDieable> _enemiesContainer;

        public Vector3 GetPosition =>
            SelectPosition();

        public PlayerRandomPosition(List<Vector3> freePoints, IObjectContainer<IDieable> enemiesContainer)
        {
            _freePoints = freePoints;
            _enemiesContainer = enemiesContainer;
        }
        private Vector3 SelectPosition()
        {
            try
            {
                var position = GetPositionFarthestFromEnemies();
                return position;
            }
            catch (Exception e)
            {
                return GetRandomPosition();
            }
        }

        private Vector3 GetRandomPosition()
        {
            int index = Random.Range(0, _freePoints.Count);
            return _freePoints[index];
        }
        private Vector3 GetPositionFarthestFromEnemies()
        {
            var enemiesCenter = EnemyCenter();


            var farthest = enemiesCenter;
            float distance = 0;
            foreach (var point in _freePoints)
            {
                var newDistance = Vector3.Distance(point, enemiesCenter);
                if (newDistance > distance)
                {
                    distance = newDistance;
                    farthest = point;
                }
            }

            return farthest;
        }
        private Vector3 EnemyCenter()
        {
            Vector3 enemiesCenter = Vector3.zero;
            var enemies = GetEnemiesPositions();
            if (enemies.Length == 0)
            {
                throw new Exception();
            }
            foreach (Vector3 enemy in enemies)
            {
                enemiesCenter += enemy;
            }
            enemiesCenter /= enemies.Length;
            return enemiesCenter;
        }
        private Vector3[] GetEnemiesPositions()
        {
            List<Vector3> positions = new List<Vector3>();
            foreach (var enemy in _enemiesContainer.GetAllObject())
            {
                if (enemy.Alive)
                {
                    positions.Add(enemy.Transform.position);
                }
            }
        
            return positions.ToArray();
        }
    }
}