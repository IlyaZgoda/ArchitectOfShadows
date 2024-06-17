using Code.Infrastructure.States;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Code.UI.Credits
{
    public class Scroller : MonoBehaviour
    {
        public float targetY;
        public float speed = 2.0f;
        private GameStateMachine _gameStateMachine;

        [Inject]
        public void Construct(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        private void Start()
        {
            StartCoroutine(MoveToTargetY());
        }

        private IEnumerator MoveToTargetY()
        {
            while (transform.position.y != targetY)
            {
                Vector3 targetPosition = new Vector3(transform.position.x, targetY, transform.position.z);
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

                yield return null;
            }

            _gameStateMachine.Enter<LoadMenuState>();
        }


    }
}
