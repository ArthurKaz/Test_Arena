using Test_Project.Abstractions;
using UnityEngine;

namespace Test_Project
{
    public class Persecutor : MonoBehaviour
    {
        [SerializeField] private float visibilityArea;
        [SerializeField] private float moveSpeed;
        private ITargetable _targetable;

        public void Init(ITargetable targetable)
        {
            _targetable = targetable;
        }

        private void FixedUpdate()
        {
            if (TargetInRange() == false) 
            {
                MoveToTarget();   
            }
        }

        private void MoveToTarget()
        {
            Vector3 target = _targetable.GetPosition();
            target.y = transform.position.y;
            transform.position =
                Vector3.MoveTowards(transform.position,target , moveSpeed * Time.deltaTime);
        }

        public bool TargetInRange()
        {
            float distance = Vector3.Distance(_targetable.GetPosition(), transform.position);
            return distance <= visibilityArea;
        }
    }

}