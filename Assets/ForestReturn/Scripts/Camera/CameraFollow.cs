using UnityEngine;

namespace ForestReturn.Scripts.Camera
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Vector3 offset;
        public float maxDistance;
        private void Start()
        {
            transform.position = target.position + offset;
        }

        private void LateUpdate()
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position + offset, maxDistance);
        }
    }
}
