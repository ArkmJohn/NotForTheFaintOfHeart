using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ShadowDetect
{
    public class ShadowDetect : MonoBehaviour
    {
        private List<Light> _lights;

        [Header("Shadow Detect Parameters")]
        [Space(10)]

        [Tooltip("Transform of GameObject to detect on shadow")]
        [SerializeField]
        private Transform _transform;
        [Tooltip("Layer that is used to selectively ignore Colliders when casting a ray")]
        [SerializeField]
        private LayerMask _layers;

        [Space(10)]
        [Header("Shadow Detect Events")]
        [Space(10)]

        [SerializeField]
        [Tooltip("Event call on change of state, enter or exit shadow")]
        private UnityEventBool _onChangeState;
        [SerializeField]
        [Tooltip("Event call if the GameObject enter in a shadow")]
        private UnityEvent _onEnterShadow;
        [SerializeField]
        [Tooltip("Event call if the GameObject exit a shadow")]
        private UnityEvent _onExitShadow;

        private bool _isOnShadow = false;
        private bool _lastValue = false;

        public bool IsOnShadow
        {
            get
            {
                return _isOnShadow;
            }

            private set
            {
                _isOnShadow = value;
            }
        }

        public List<Light> Lights
        {
            get
            {
                return _lights;
            }

            set
            {
                _lights = value;
            }
        }

        public UnityEventBool OnChangeState
        {
            get
            {
                return _onChangeState;
            }

            private set
            {
                _onChangeState = value;
            }
        }

        public UnityEvent OnEnterShadow
        {
            get
            {
                return _onEnterShadow;
            }

            private set
            {
                _onEnterShadow = value;
            }
        }

        public UnityEvent OnExitShadow
        {
            get
            {
                return _onExitShadow;
            }

            private set
            {
                _onExitShadow = value;
            }
        }

        void Awake()
        {
            //If you won't to call a "Find" function on Awake,
            //You can comment this awake function, serialize Lights Members and drag/drop lights on inspector
            Lights = new List<Light>();
            Lights = FindObjectsOfType<Light>().ToList();
            if (_transform == null)
                _transform = gameObject.transform;
        }

        // Update is called once per frame
        void Update()
        {
            if (Lights == null)
                return;

            //Browse the list of lights
            for (int i = 0; i < Lights.Count; ++i)
            {
                IsOnShadow = !IsOnLight(Lights[i]);
                if (!IsOnShadow)
                    break;
            }

            //Call Events
            if (_lastValue != IsOnShadow)
                OnChangeState.Invoke(IsOnShadow);
            if (IsOnShadow)
                OnEnterShadow.Invoke();
            else
                OnExitShadow.Invoke();

            _lastValue = IsOnShadow;
        }

        /// <summary>
        /// Detect if your character is on Light
        /// </summary>
        /// <param name="light"></param>
        /// <returns></returns>
        public bool IsOnLight(Light light)
        {
            if (!light.isActiveAndEnabled)
                return false;

            //0: Spot - 1: Directional - 2: Point
            switch ((int)light.type)
            {
                case 0:
                    return IsOnSpotlLight(light);
                case 1:
                    return IsOnDirectionalLight(light);
                case 2:
                    return IsOnPointLight(light);
                default:
                    return false;
            }
        }

        bool IsOnDirectionalLight(Light light)
        {
            if (light.intensity == 0)
                return false;

            RaycastHit hit;
            Ray ray = new Ray(_transform.position, -light.transform.forward);
#if UNITY_EDITOR
            Debug.DrawRay(ray.origin, ray.direction, Color.red);
#endif
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~_layers))
            {
                // Do Stuff if you want
                return false;
            }

            return true;
        }
        bool IsOnSpotlLight(Light light)
        {
            if (light.intensity == 0)
                return false;

            Vector3 FromLightToCharacter = _transform.position - light.transform.position;

            if (FromLightToCharacter.magnitude > light.range * (light.intensity / 10.0f) || Vector3.Angle(light.transform.forward, FromLightToCharacter) > light.spotAngle / 2.0f)
                return false;

            RaycastHit hit;
            Ray ray = new Ray(light.transform.position, FromLightToCharacter);

#if UNITY_EDITOR
            Debug.DrawRay(ray.origin, ray.direction, Color.blue);
#endif

            if (Physics.Raycast(ray, out hit, FromLightToCharacter.magnitude, ~_layers))
            {
                // Do Stuff if you want
                return false;
            }

            return true;
        }
        bool IsOnPointLight(Light light)
        {
            if (light.intensity == 0)
                return false;

            Vector3 FromCharacterToLight = light.transform.position - _transform.position;

            if (FromCharacterToLight.magnitude >= light.range * (light.intensity / 10.0f))
                return false;

            RaycastHit hit;
            Ray ray = new Ray(_transform.position, FromCharacterToLight);

#if UNITY_EDITOR
            Debug.DrawRay(ray.origin, ray.direction, Color.blue);
#endif

            if (Physics.Raycast(ray, out hit, FromCharacterToLight.magnitude, ~_layers))
            {
                // Do Stuff if you want
                return false;
            }

            return true;
        }

        // Class Event with bool on parameter (and visible on inspector)
        [System.Serializable]
        public class UnityEventBool : UnityEvent<bool> { }
    }
}
