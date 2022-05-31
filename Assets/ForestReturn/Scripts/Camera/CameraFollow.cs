using UnityEngine;

namespace ForestReturn.Scripts.Camera
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform target;
        private Vector3 offset;
        public float maxDistance;
        private void Start()
        {
            offset = transform.position - target.position;
        }

        private void LateUpdate()
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position + offset, maxDistance);
        }
    }
}
