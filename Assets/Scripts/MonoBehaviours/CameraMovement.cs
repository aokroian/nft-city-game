using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace MonoBehaviours
{
    public class CameraMovement : MonoBehaviour
    {
        #region Inspector

        [SerializeField] private Camera mainCamera;
        [SerializeField] private float movementSpeed = 5f;

        public UnityEvent<bool> onCameraStoppedMoving;

        public Transform anchorForCameraMovementBounds;
        public float maxRadiusFromCenter = 200f;

        #endregion

        #region Fields

        private bool _mousePressed;
        private Vector3 _mouseInput;
        private float _mouseDeltaMagnitude;
        private Vector3 _currentPosOnPlane;
        private Vector3 _dragStartPosition;

        private Vector3 _targetPosition;
        private bool _isMoving;
        private bool _isAllowedToMove = true;

        #endregion

        #region InputSystemCallbacks

        public void OnMouseClick(InputAction.CallbackContext callbackContext)
        {
            if (callbackContext.performed)
            {
                HandleMousePressed();
            }
            else if (callbackContext.canceled)
            {
                HandleMouseReleased();
            }
        }

        public void OnMousePosition(InputAction.CallbackContext callbackContext)
        {
            if (!_isAllowedToMove) return;
            var position = callbackContext.ReadValue<Vector2>();
            _mouseInput = position;
        }

        public void OnMouseDelta(InputAction.CallbackContext callbackContext)
        {
            if (!_isAllowedToMove) return;
            var delta = callbackContext.ReadValue<Vector2>();
            _mouseDeltaMagnitude = delta.magnitude;
        }

        #endregion

        #region MonoBehaviour

        private void Start()
        {
            onCameraStoppedMoving ??= new UnityEvent<bool>();
            _dragStartPosition = new Vector3();
            _currentPosOnPlane = new Vector3();
            _isMoving = false;
        }

        private void LateUpdate()
        {
            if (!_isAllowedToMove) return;

            // move part
            var currentPosition = transform.position;
            if (_mousePressed)
            {
                SetPosOnPlane();
                _targetPosition = currentPosition + (_dragStartPosition - _currentPosOnPlane);
            }

            if (anchorForCameraMovementBounds != null)
            {
                var anchor = anchorForCameraMovementBounds.position;
                var dist = Vector3.Distance(anchor, _targetPosition);

                if (dist > maxRadiusFromCenter)
                {
                    var newTarget = anchor + (_targetPosition - anchor).normalized * (maxRadiusFromCenter - 10);
                    transform.position = Vector3.Lerp(currentPosition, newTarget, Time.deltaTime * movementSpeed);
                }
                else
                {
                    transform.position = Vector3.Lerp(currentPosition, _targetPosition, Time.deltaTime * movementSpeed);
                }
            }
            else
            {
                transform.position = Vector3.Lerp(currentPosition, _targetPosition, Time.deltaTime * movementSpeed);
            }

        

            // event part
            if (_mouseDeltaMagnitude >= 0.7f && _mousePressed)
            {
                if (!_isMoving) onCameraStoppedMoving.Invoke(false);
                _isMoving = true;
            }
            else if (!_mousePressed)
            {
                if (_isMoving) onCameraStoppedMoving.Invoke(true);
                _isMoving = false;
            }
        }

        private void SetPosOnPlane()
        {
            mainCamera ??= Camera.main;
            var plane = new Plane(Vector3.up, Vector3.zero);
            var ray = mainCamera.ScreenPointToRay(_mouseInput);
            if (!plane.Raycast(ray, out var entry)) return;
            _currentPosOnPlane = ray.GetPoint(entry);
            _currentPosOnPlane.y = 0;
        }

        private void HandleMousePressed()
        {
            if (!_isAllowedToMove) return;
            _mousePressed = true;
            SetPosOnPlane();
            _dragStartPosition = _currentPosOnPlane;
        }

        private void HandleMouseReleased()
        {
            if (!_isAllowedToMove) return;
            _mousePressed = false;
        }

        public void InvertedToggleCameraMovement(bool disable)
        {
            _isAllowedToMove = !disable;
        }

        #endregion
    }
}