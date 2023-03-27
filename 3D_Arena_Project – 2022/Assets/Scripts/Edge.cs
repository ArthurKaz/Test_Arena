using Test_Project.Abstractions;
using Test_Project.TestEnemy;
using UnityEngine;

namespace Test_Project
{
    public class Edge : MonoBehaviour
    {
        private IRandomPosition _randomPosition;

        public void Init(IRandomPosition randomPosition)
        {
            _randomPosition = randomPosition;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out TargetAble player))
            {
                player.OnReplaced();
                player.transform.position = _randomPosition.GetPosition;
            }
        }
    }
}