using System.Collections.Generic;
using UnityEngine;

namespace ForestReturn.Scripts.Cenario
{
    public class TrapLaser : MonoBehaviour
    {
        [SerializeField] private GameObject laserGameObject;
        [SerializeField] private bool isClockwise = true;
        private readonly List<Material[]> _materialsRefs = new();
        private Animator _animator;
        private static readonly int IsClockwise = Animator.StringToHash("isClockwise");

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _animator.SetBool(IsClockwise,isClockwise);
            var meshRenderers = GetComponentsInChildren<MeshRenderer>();
            foreach (var meshRendererChild in meshRenderers)
            {
                _materialsRefs.Add(meshRendererChild.GetComponent<MeshRenderer>().materials);
            }
            _materialsRefs.Add(GetComponent<MeshRenderer>().materials);
        }

        public void ChangeState(bool state)
        {
            if (state)
            {
                laserGameObject.SetActive(true);
                _animator.enabled = true;
                foreach (Material[] materialsRef in _materialsRefs)
                {
                    materialsRef[0].EnableKeyword("_EMISSION");
                }
            }
            else
            {
                laserGameObject.SetActive(false);
                _animator.enabled = false;
                foreach (Material[] materialsRef in _materialsRefs)
                {
                    materialsRef[0].DisableKeyword("_EMISSION");
                }
            }
        }
    }
}
