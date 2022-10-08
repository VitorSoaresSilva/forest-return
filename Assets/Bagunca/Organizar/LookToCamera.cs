using UnityEngine;

namespace Bagunca.Organizar
{
    public class LookToCamera : MonoBehaviour
    {
        private Transform _cameraLocation;

        [SerializeField] private GameObject rotationObject;
        // Start is called before the first frame update
        void Start()
        {
            // if (LevelManager.instance != null)
            // {
            //     _cameraLocation = LevelManager.instance.CameraFollow.gameObject.transform;
            // }
            // else
            // {
            //     enabled = false;
            // }
        }

        // Update is called once per frame
        void Update()
        {
            rotationObject.transform.LookAt(_cameraLocation);
        }
    }
}
