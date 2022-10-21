using ForestReturn.Scripts.Managers;
using UnityEngine;

namespace ForestReturn.Scripts
{
    public class LookToCamera : MonoBehaviour
    {
        private Transform _cameraLocation;

        [SerializeField] private GameObject rotationObject;
        // Start is called before the first frame update
        void Start()
        {
            _cameraLocation = LevelManager.instance.CamerasHolder.mainCamera.transform;
        }

        // Update is called once per frame
        void Update()
        {
            rotationObject.transform.LookAt(_cameraLocation);
        }
    }
}
