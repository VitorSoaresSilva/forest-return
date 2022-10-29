using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ForestReturn.Scripts.Skills.Vines
{
    public class VinesPowerUp : MonoBehaviour
    {
        public List<MeshRenderer> growVinesMeshes;
        public float timeToGrowForward = 0.8f;
        public float timeToGrowBackward = 1.5f;
        public float timeToStayActive = 0.5f;
        public float refreshRate = 0.05f;
        [Range(0, 1)] public float minGrow = 0.2f;
        [Range(0, 1)] public float maxGrow = 0.97f;

        private readonly List<Material> _growVinesMaterials = new();
        private bool _fullyGrown;
        private static readonly int Grow = Shader.PropertyToID("_Grow");
        [SerializeField] private GameObject colliderGameObject;
        private float _maxColliderZValue;

        private void Awake()
        {
            if (colliderGameObject == null)
            {
                colliderGameObject = GetComponentInChildren<BoxCollider>().gameObject;
            }

            var localScale = colliderGameObject.transform.localScale;
            _maxColliderZValue = localScale.z;
            localScale = new Vector3(localScale.x, localScale.y, 0);
            colliderGameObject.transform.localScale = localScale;
            foreach (var mesh in growVinesMeshes)
            {
                foreach (var material in mesh.materials)
                {
                    if (material.HasProperty(Grow))
                    {
                        material.SetFloat(Grow, minGrow);
                        _growVinesMaterials.Add(material);
                    }
                }
            }

            colliderGameObject.SetActive(false);
        }

        private void Start()
        {
            GrowUp();
        }

        [ContextMenu("GrowUp")]
        public void GrowUp()
        {
            StartCoroutine(GrowVines());
            
        }

        private IEnumerator GrowVines()
        {
            float growValue = 0;
            var localScale = colliderGameObject.transform.localScale;
            // if (!_fullyGrown)
            // {
            colliderGameObject.SetActive(true);
            while (growValue < maxGrow)
            {
                growValue += 1 / (timeToGrowForward / refreshRate);
                foreach (var material in _growVinesMaterials)
                {
                    material.SetFloat(Grow, growValue);
                }
                localScale = new Vector3(localScale.x, localScale.y, Mathf.Lerp(0, _maxColliderZValue, growValue));
                colliderGameObject.transform.localScale = localScale;
                yield return new WaitForSeconds(refreshRate);
            }
            localScale = new Vector3(localScale.x, localScale.y, _maxColliderZValue);
            colliderGameObject.transform.localScale = localScale;
            Debug.Log("Fully grown");
            yield return new WaitForSeconds(timeToStayActive);
            while (growValue > minGrow)
            {
                growValue -= 1 / (timeToGrowBackward / refreshRate);
                foreach (var material in _growVinesMaterials)
                {
                    material.SetFloat(Grow, growValue);
                }
                localScale = new Vector3(localScale.x, localScale.y, Mathf.Lerp(0, _maxColliderZValue, growValue));
                colliderGameObject.transform.localScale = localScale;
                yield return new WaitForSeconds(refreshRate);
            }
            colliderGameObject.SetActive(false);
            
            
            
            // }
            // else
            // {
            //     while (growValue > minGrow)
            //     {
            //         growValue -= 1 / (timeToGrowBackward / refreshRate);
            //         material.SetFloat(Grow, growValue);
            //         localScale = new Vector3(localScale.x, localScale.y, Mathf.Lerp(0, _maxColliderZValue, growValue));
            //         colliderGameObject.transform.localScale = localScale;
            //         yield return new WaitForSeconds(refreshRate);
            //     }
            //
            //     colliderGameObject.SetActive(false);
            // }

            _fullyGrown = growValue >= maxGrow;
        }

        private IEnumerator GrowVines2(Material material)
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
                    localScale = new Vector3(localScale.x, localScale.y, Mathf.Lerp(0, _maxColliderZValue, growValue));
                    colliderGameObject.transform.localScale = localScale;

                    yield return new WaitForSeconds(refreshRate);
                }

                Debug.Log("Fully grown");
            }
            else
            {
                while (growValue > minGrow)
                {
                    growValue -= 1 / (timeToGrowBackward / refreshRate);
                    material.SetFloat(Grow, growValue);
                    localScale = new Vector3(localScale.x, localScale.y, Mathf.Lerp(0, _maxColliderZValue, growValue));
                    colliderGameObject.transform.localScale = localScale;
                    yield return new WaitForSeconds(refreshRate);
                }

                colliderGameObject.SetActive(false);
            }

            _fullyGrown = growValue >= maxGrow;
        }
    }
}