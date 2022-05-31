using System;
using System.Collections;
using UnityEngine;

namespace ForestReturn.Scripts.Traps
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

        private void Start()
        {
            movingPart.localPosition =
                new Vector3(movingPart.localPosition.x, movingPart.localPosition.y, minPosition);
            _totalTimeForward = forward[forward.length - 1].time;
            _totalTimeBackward = backward[backward.length - 1].time;
            Invoke(nameof(StartMoving), coolDown);
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
            while (time < _totalTimeForward)
            {
                var newPosition = Mathf.Lerp(minPosition, maxPosition, forward.Evaluate(time));
                time += Time.fixedDeltaTime * speedMultiplier;
                movingPart.localPosition =
                    new Vector3(movingPart.localPosition.x, movingPart.localPosition.y, newPosition);
                yield return new WaitForFixedUpdate();
            }

            yield return new WaitForSeconds(waitTimeBeforeRetract);
            _coroutine = StartCoroutine(nameof(RetractCoroutine));
            yield return null;
        }

        private IEnumerator RetractCoroutine()
        {
            float time = 0;
            while (time < _totalTimeBackward)
            {
                var newPosition = Mathf.Lerp(maxPosition, minPosition, backward.Evaluate(time));
                time += Time.fixedDeltaTime * speedMultiplier;
                movingPart.localPosition =
                    new Vector3(movingPart.localPosition.x, movingPart.localPosition.y, newPosition);
                yield return new WaitForFixedUpdate();
            }

            yield return new WaitForSeconds(coolDown);
            _coroutine = StartCoroutine(nameof(Active));
            yield return null;
        }
    }
}