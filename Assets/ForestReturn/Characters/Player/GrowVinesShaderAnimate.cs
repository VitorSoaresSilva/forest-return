using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ForestReturn.Characters.Player
{
    public class GrowVinesShaderAnimate : MonoBehaviour
    {
        public List<MeshRenderer> growVinesMeshes;
        public float timeToGrowForward = 0.8f;
        public float timeToGrowBackward = 1.5f;
        public float refreshRate = 0.05f;
        [Range(0, 1)] public float minGrow = 0.2f;
        [Range(0, 1)] public float maxGrow = 0.97f;

        private readonly List<Material> _growVinesMaterials = new();
        private bool _fullyGrown;
        private static readonly int Grow = Shader.PropertyToID("_Grow");

        [SerializeField] private GameObject colliderGameObject;
        private float _maxColliderZValue;
        private void Start()
        {
            var localScale = colliderGameObject.transform.localScale;
            _maxColliderZValue = localScale.z;
            colliderGameObject.SetActive(false);
            localScale = new Vector3(localScale.x, localScale.y, 0);
            colliderGameObject.transform.localScale = localScale;
            foreach (var mesh in growVinesMeshes)
            {
                foreach (var material in mesh.materials)
                {
                    if (material.HasProperty(Grow))
                    {
                        material.SetFloat(Grow,minGrow);
                        _growVinesMaterials.Add(material);
                    }
                }
            }
        }

        [ContextMenu("GrowUp")]
        public void GrowUp()
        {
            foreach (var material in _growVinesMaterials)
            {
                StartCoroutine(GrowVines(material));
            }
        }

        private IEnumerator GrowVines(Material material)
        {
            float growValue = material.GetFloat(Grow);
            var localScale = colliderGameObject.transform.localScale;
            if (!_fullyGrown)
            {
                colliderGameObject.SetActive(true);
                while (growValue < maxGrow)
                {
                    growValue += 1 / (timeToGrowForward / refreshRate);
                    material.SetFloat(Grow, growValue);
                    localScale = new Vector3(localScale.x,localScale.y,Mathf.Lerp(0,_maxColliderZValue,growValue));
                    colliderGameObject.transform.localScale = localScale;

                    yield return new WaitForSeconds(refreshRate);
                }
            }
            else
            {
                while (growValue > minGrow)
                {
                    growValue -= 1 / (timeToGrowBackward / refreshRate);
                    material.SetFloat(Grow, growValue);
                    localScale = new Vector3(localScale.x,localScale.y,Mathf.Lerp(0,_maxColliderZValue,growValue));
                    colliderGameObject.transform.localScale = localScale;
                    yield return new WaitForSeconds(refreshRate);
                }
                colliderGameObject.SetActive(false);
            }

            _fullyGrown = growValue >= maxGrow;
        }
    }
}
