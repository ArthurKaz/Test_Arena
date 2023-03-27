using System.Collections;
using UnityEngine;

namespace Test_Project.Entities
{
    public class RedEnemyAttackBehaviour : EnemyBehaviour
    {
        [SerializeField] private float moveUpSpeed = 5f;
        [SerializeField] private float moveToTargetSpeed = 5f;
        [SerializeField] private float upDistance = 5f;
        [SerializeField] private int damage = 15;
        [SerializeField] private float hangingInAirTime = 1;
        
        private IEnumerator _movingUp;
        private IEnumerator _movingToTarget;
        private IEnumerator _movingToPosition;
        
        public override void StartAttack()
        {
            if (_movingUp != null)
            {
                StopCoroutine(_movingUp);
            }

            if (_movingToTarget != null)
            {
                StopCoroutine(_movingToTarget);
            }
            
            _movingUp = MoveUp();
            _movingToTarget = MoveToTarget();
            StartCoroutine(_movingUp);
        }

        public override void StopAttack()
        {
            if (_movingUp != null)
            {
                StopCoroutine(_movingUp);
            }

            if (_movingToTarget != null)
            {
                StopCoroutine(_movingToTarget);
            }
        }


       
        private IEnumerator MoveUp()
        {
            transform.rotation = Quaternion.identity;
            Vector3 targetPosition = transform.position + Vector3.up * upDistance;
            yield return  StartCoroutine(MoveToPosition(targetPosition, moveUpSpeed));
            yield return new WaitForSeconds(hangingInAirTime);
            transform.LookAt(Targetable.GetPosition());
            yield return StartCoroutine(_movingToTarget);
        }

        private IEnumerator MoveToTarget()
        {
            Vector3 direction = Targetable.GetPosition() - transform.position;
            direction.Normalize();
            yield return StartCoroutine(MoveToPosition(Targetable.GetPosition(), moveToTargetSpeed));
            Destroy();
        }
        private IEnumerator MoveToPosition(Vector3 targetPosition, float moveSpeed)
        {
            if (_movingToPosition != null)
            {
                StopCoroutine(_movingToPosition);
            }

            _movingToPosition = MoveToTargetPosition(targetPosition, moveSpeed);
            return _movingToPosition;
        }

        private IEnumerator MoveToTargetPosition(Vector3 targetPosition, float moveSpeed)
        {
            float distance = Vector3.Distance(transform.position, targetPosition);

            while (distance > 0.1f)
            {

                transform.position =
                    Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                distance = Vector3.Distance(transform.position, targetPosition);
                yield return null;
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent(out Player player))
            {
                player.TakeDamage(damage);
            }

            Destroy();
        }

        private void Destroy()
        {
            if (Dieable.Alive)
            {
                Dieable.TakeDamage(float.MaxValue);
            }
        }
    }
}