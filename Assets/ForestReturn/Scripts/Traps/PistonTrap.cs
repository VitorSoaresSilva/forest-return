using System;
using System.Collections;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using STOP_MODE = FMOD.Studio.STOP_MODE;

namespace Traps
{
    public class PistonTrap : MonoBehaviour
    {
        public AnimationCurve forward;
        public AnimationCurve backward;
        public float minPosition;
        public float maxPosition;
        public Transform movingPart;
        public float coolDown;
        public float waitTimeBeforeRetract;
        public float speedMultiplier = 1;
        private float _totalTimeForward;
        private float _totalTimeBackward;
        private Coroutine _coroutine;

        [Header("Audio")] 
        [SerializeField] private EventReference activeEventPath;
        [SerializeField] private EventReference retractEventPath;
        private EventInstance activeEventInstance;
        private EventInstance retractEventInstance;
        private void Start()
        {
            movingPart.localPosition =
                new Vector3(movingPart.localPosition.x, movingPart.localPosition.y, minPosition);
            _totalTimeForward = forward[forward.length - 1].time;
            _totalTimeBackward = backward[backward.length - 1].time;
            Invoke(nameof(StartMoving), coolDown);
            activeEventInstance = RuntimeManager.CreateInstance(activeEventPath);
            retractEventInstance = RuntimeManager.CreateInstance(retractEventPath);
            RuntimeManager.AttachInstanceToGameObject(activeEventInstance,transform);
            RuntimeManager.AttachInstanceToGameObject(retractEventInstance,transform);
        }

        public void StartMoving()
        {
            _coroutine = StartCoroutine(nameof(Active));
        }
        
        public void StopAll()
        {
            StopCoroutine(_coroutine);
        }

        private IEnumerator Active()
        {
            float time = 0;
            activeEventInstance.start();
            while (time < _totalTimeForward)
            {
                var newPosition = Mathf.Lerp(minPosition, maxPosition, forward.Evaluate(time));
                time += Time.fixedDeltaTime * speedMultiplier;
                movingPart.localPosition =
                    new Vector3(movingPart.localPosition.x, movingPart.localPosition.y, newPosition);
                yield return new WaitForFixedUpdate();
            }

            activeEventInstance.stop(STOP_MODE.IMMEDIATE);
            yield return new WaitForSeconds(waitTimeBeforeRetract);
            _coroutine = StartCoroutine(nameof(RetractCoroutine));
            yield return null;
        }

        private IEnumerator RetractCoroutine()
        {
            float time = 0;
            retractEventInstance.start();
            while (time < _totalTimeBackward)
            {
                var newPosition = Mathf.Lerp(maxPosition, minPosition, backward.Evaluate(time));
                time += Time.fixedDeltaTime * speedMultiplier;
                movingPart.localPosition =
                    new Vector3(movingPart.localPosition.x, movingPart.localPosition.y, newPosition);
                yield return new WaitForFixedUpdate();
            }

            retractEventInstance.stop(STOP_MODE.IMMEDIATE);
            yield return new WaitForSeconds(coolDown);
            _coroutine = StartCoroutine(nameof(Active));
            yield return null;
        }
    }
}