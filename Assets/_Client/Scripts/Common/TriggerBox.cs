using System;
using UnityEngine;

namespace Common
{
    public class TriggerBox : MonoBehaviour
    {
        private string _colliderTag;
        private Action<Collider> _onTriggerEnter;
        private Action<Collider> _onTriggerExit;
        private Action<Collider> _onTriggerStay;

        public void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(_colliderTag))
                _onTriggerEnter(other);
        }

        public void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(_colliderTag))
                _onTriggerExit(other);
        }

        public void OnTriggerStay(Collider other)
        {
            if (other.CompareTag(_colliderTag))
                _onTriggerStay(other);
        }

        public void Initialize(Action<Collider> onTriggerEnter, Action<Collider> onTriggerExit,
            Action<Collider> onTriggerStay, string colliderTag)
        {
            _onTriggerEnter = onTriggerEnter;
            _onTriggerExit = onTriggerExit;
            _onTriggerStay = onTriggerStay;
            _colliderTag = colliderTag;
        }
    }
}