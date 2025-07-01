using UnityEngine;

namespace Common
{
    public class Database : ScriptableObject
    {
        [SerializeField] private string _id;
        public string Id => _id;
    }
}