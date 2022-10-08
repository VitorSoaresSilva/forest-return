using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace _Developers.Vitor.Scripts.Enemies
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class SimpleNavmeshAgentMove : MonoBehaviour
    {
        [SerializeField] private Transform[] points;
        private NavMeshAgent _navMeshAgent;
        private Transform _target;
        private Animator _animator;
        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _navMeshAgent.destination = points[Random.Range(0, points.Length)].position;
        }
        // Update is called once per frame
        void Update()
        {
            if (_animator != null)
            {
                _animator.SetBool("isMoving",_navMeshAgent.velocity.magnitude > 0.01f);
            }
            if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
            {
                _navMeshAgent.destination = points[Random.Range(0, points.Length)].position;
            }
        }
    }
}
