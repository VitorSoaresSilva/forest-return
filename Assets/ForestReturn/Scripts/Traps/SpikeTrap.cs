using System.Collections;
using Unity.EditorCoroutines.Editor;
using UnityEngine;
namespace ForestReturn.Scripts.Traps
{
    public class SpikeTrap : MonoBehaviour
    {
        public AnimationCurve positionCurve;
        public float minHeight;
        public float maxHeight;
        public float speedMultiplier = 1;
        public float waitTimeBeforeRetract = 1;
        public float coolDown = 2;
        public Transform movingPart;
        private float _totalTime;
        private bool _reloaded = true;
        
        private void Start()
        {
            movingPart.localPosition = new Vector3(movingPart.localPosition.x,minHeight,movingPart.localPosition.z);
            _totalTime = positionCurve[positionCurve.length - 1].time;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_reloaded)
            {
                Active();
            }
        }

        public void EditorActive()
        {
            if (_reloaded)
            {
                Active();
            }
        }
        public void Active()
        {
            _reloaded = false;
            StartCoroutine(nameof(ActiveCoroutine));
        }

        private IEnumerator ActiveCoroutine()
        {
            float time = 0;
            while (time < _totalTime)
            {
                var newHeight = Mathf.Lerp(minHeight, maxHeight, positionCurve.Evaluate(time));
                time += Time.fixedDeltaTime * speedMultiplier;
                movingPart.localPosition = new Vector3(movingPart.localPosition.x,newHeight,movingPart.localPosition.z);
                yield return new WaitForFixedUpdate();
            }
            yield return new WaitForSeconds(waitTimeBeforeRetract); 
            StartCoroutine(nameof(RetractCoroutine));
            yield return null;
        }

        private IEnumerator RetractCoroutine()
        {
            float time = 0;
            while (time < _totalTime)
            {
                var newHeight = Mathf.Lerp(maxHeight, minHeight, positionCurve.Evaluate(time));
                time += Time.fixedDeltaTime * speedMultiplier;
                movingPart.localPosition = new Vector3(movingPart.localPosition.x,newHeight,movingPart.localPosition.z);
                yield return new WaitForFixedUpdate();
            }
            yield return new WaitForSeconds(coolDown);
            _reloaded = true;
            yield return null;
        }
    }
}
