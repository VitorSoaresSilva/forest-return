using UnityEngine;

namespace _Developers.Vitor.Scripts.Camera
{
    public class CameraFollow : MonoBehaviour
    {
        public Transform target = default;
        [SerializeField] private Vector3 offset;
        public float maxDistance;
        private void Start()
        {
            if (target != null)
            { 
                SetPosition();
            }
        }

        public void SetPosition()
        {
            transform.position += target.position;
        }
        private void LateUpdate()
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position + offset, maxDistance);
        }
    }
}
