using System;
using ForestReturn.Scripts.Managers;
using UnityEngine;

namespace ForestReturn.Scripts.Shaders
{
    public class CutoutObject : MonoBehaviour
    {
        [SerializeField] private Transform targetObject;
        [SerializeField] private LayerMask wallMask;
        private Camera _mainCamera;
        private static readonly int FallOfSize = Shader.PropertyToID("_FallofSize");
        private static readonly int CutoutSize = Shader.PropertyToID("_CutoutSize");
        private static readonly int CutoutPos = Shader.PropertyToID("_CutoutPos");

        private void Awake()
        {
            _mainCamera = LevelManager.Instance.CamerasHolder.mainCamera;
        }

        private void Update()
        {
            Vector2 cutoutPos = _mainCamera.WorldToViewportPoint(targetObject.position);
            cutoutPos.y /= (Screen.width / Screen.height);
            Vector3 offset = targetObject.position - transform.position;
            RaycastHit[] hitObjects = Physics.RaycastAll(transform.position, offset, offset.magnitude, wallMask);
            for (int i = 0; i < hitObjects.Length; i++)
            {
                Material[] materials = hitObjects[i].transform.GetComponent<Renderer>().materials;
                for (int j = 0; j < materials.Length; j++)
                {
                    materials[j].SetVector(CutoutPos,cutoutPos);
                    materials[j].SetFloat(CutoutSize,0.1f);
                    materials[j].SetFloat(FallOfSize,0.05f);
                }
            }
        }
    }
}