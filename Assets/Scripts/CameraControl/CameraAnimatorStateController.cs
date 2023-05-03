using UnityEngine;

namespace CameraControl
{
    public class CameraAnimatorStateController : MonoBehaviour
    {
        private Animator _targetAnimator;

        private readonly string[] _statesNames = new[]
        {
            "Stack0",
            "Stack1",
            "Stack2"
        };
        
        private void Start()
        {
            _targetAnimator = GetComponent<Animator>();
        }

        public void SetAnimationState(int stateIndex)
        {
            _targetAnimator.CrossFade(_statesNames[stateIndex], 10f);
            
        }

        
    }
}
