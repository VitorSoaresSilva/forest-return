using UnityEngine;

namespace Traps
{
    public class WindTrap : MonoBehaviour
    {
        public float force;
        public bool useConstantForce;
        private BoxCollider _boxCollider;
        private float _maxDistance;
        private void Awake()
        {
            _boxCollider = GetComponent<BoxCollider>();
            _maxDistance = 1 / (_boxCollider.size.z * transform.localScale.z);
        }
        private void OnTriggerStay(Collider other)
        {
            if (!other.attachedRigidbody) return;
            var distanceMultiplier = useConstantForce ? 1f : 1 - Vector3.Distance(other.transform.position, transform.position) * _maxDistance;
            other.attachedRigidbody.AddForce(transform.forward * force * distanceMultiplier, ForceMode.Force);
        }
    }
}