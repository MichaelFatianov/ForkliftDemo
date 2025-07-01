using System;
using UnityEngine;

namespace _Client.Scripts
{
    public class TriggerBox : MonoBehaviour
    {
        private Action<Collider> _onTriggerEnter;
        private Action<Collider> _onTriggerStay;
        private Action<Collider> _onTriggerExit;
        private string _colliderTag;

        public void Initialize(Action<Collider> onTriggerEnter, Action<Collider> onTriggerExit, Action<Collider> onTriggerStay, string colliderTag)
        {
            _onTriggerEnter = onTriggerEnter;
            _onTriggerExit = onTriggerExit;
            _onTriggerStay = onTriggerStay;
            _colliderTag = colliderTag;
        }

        public void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag(_colliderTag))
                _onTriggerEnter(other);
        }

        public void OnTriggerStay(Collider other)
        {
            if(other.CompareTag(_colliderTag))
                _onTriggerStay(other);
        }

        public void OnTriggerExit(Collider other)
        {
            if(other.CompareTag(_colliderTag))
                _onTriggerExit(other);
        }
    }
}