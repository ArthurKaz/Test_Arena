using System.Collections.Generic;
using Test_Project.Abstractions;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Test_Project
{
    public class EnemyRandomPosition : IRandomPosition
    {
        private readonly List<Vector3> _points;
        private Queue<Vector3> _snuffledPoints;
        private LayerMask _obstacleMasks;
        public Vector3 GetPosition => PullSnaffledPoint();
        public EnemyRandomPosition(List<Vector3> points, LayerMask mask)
        {
            _points = points;
            _obstacleMasks = mask;
            SnufflePoints();
        }
        /*
         *To obtain a random spot on the map that is still available, a queue is
         * created from a shuffled list of points to place objects. Then, in a loop,
         * a point is retrieved from the queue and checked if it is free. If the point
         * is free, it is returned, otherwise the loop continues until a free point is
         * found. The SnufflePoints() method shuffles the list of points and creates a
         * queue from it, while the FreePoint() method checks if a given point is free
         * by checking if there are any colliders within a certain radius around it.
         * This code uses a common algorithm for selecting a random spot on the map
         * while ensuring that it is not occupied by any other objects. The
         * SnufflePoints() method shuffles the list of points to avoid any bias in
         * the selection of points, while the FreePoint() method checks for any nearby
         * colliders to determine if the spot is free.
         */
        private Vector3 PullSnaffledPoint()
        {
            if (_snuffledPoints.Count == 0)
            {
                SnufflePoints();
            }
            Vector3 position = _points[0];
            for (var index = 0; index < _snuffledPoints.Count; index++)
            {
                var point = _snuffledPoints.Dequeue();
                if (FreePoint(point))
                {
                    return point;
                }
            }

            return position;
        }

        private void  SnufflePoints()
        {
            List<Vector3> shuffledList = new List<Vector3>(_points);

            for (int i = shuffledList.Count - 1; i > 0; i--)
            {
                int randomIndex = Random.Range(0, i + 1);
                (shuffledList[randomIndex], shuffledList[i]) = (shuffledList[i], shuffledList[randomIndex]);
            }

            _snuffledPoints = new Queue<Vector3>(shuffledList);
        }

        private bool FreePoint(Vector3 position)
        {
            var needSpaceInRadius = 0.5f;
            var colliders = Physics.OverlapSphere(position, needSpaceInRadius, _obstacleMasks);
            return colliders.Length == 0;
        }
    }
}