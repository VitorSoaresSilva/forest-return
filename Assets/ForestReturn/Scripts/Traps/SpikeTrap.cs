using System.Collections;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using STOP_MODE = FMOD.Studio.STOP_MODE;

namespace Traps
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
        
        [Header("Audio")]
        [SerializeField] private EventReference activeEventPath;
        [SerializeField] private EventReference retractEventPath;
        private EventInstance activeEventInstance;
        private EventInstance retractEventInstance;
        private void Start()
        {
            movingPart.localPosition = new Vector3(movingPart.localPosition.x,minHeight,movingPart.localPosition.z);
            _totalTime = positionCurve[positionCurve.length - 1].time;
            activeEventInstance = RuntimeManager.CreateInstance(activeEventPath);
            retractEventInstance = RuntimeManager.CreateInstance(retractEventPath);
            RuntimeManager.AttachInstanceToGameObject(activeEventInstance,transform);
            RuntimeManager.AttachInstanceToGameObject(retractEventInstance,transform);
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
            activeEventInstance.start();
            while (time < _totalTime)
            {
                var newHeight = Mathf.Lerp(minHeight, maxHeight, positionCurve.Evaluate(time));
                time += Time.fixedDeltaTime * speedMultiplier;
                movingPart.localPosition = new Vector3(movingPart.localPosition.x,newHeight,movingPart.localPosition.z);
                yield return new WaitForFixedUpdate();
            }
            activeEventInstance.stop(STOP_MODE.IMMEDIATE);
            yield return new WaitForSeconds(waitTimeBeforeRetract); 
            StartCoroutine(nameof(RetractCoroutine));
            yield return null;
        }

        private IEnumerator RetractCoroutine()
        {
            float time = 0;
            retractEventInstance.start();
            while (time < _totalTime)
            {
                var newHeight = Mathf.Lerp(maxHeight, minHeight, positionCurve.Evaluate(time));
                time += Time.fixedDeltaTime * speedMultiplier;
                movingPart.localPosition = new Vector3(movingPart.localPosition.x,newHeight,movingPart.localPosition.z);
                yield return new WaitForFixedUpdate();
            }
            retractEventInstance.stop(STOP_MODE.IMMEDIATE);
            yield return new WaitForSeconds(coolDown);
            _reloaded = true;
            yield return null;
        }
    }
}
