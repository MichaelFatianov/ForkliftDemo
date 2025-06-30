using UnityEngine;

namespace _Client.Scripts
{
    public class Database: ScriptableObject
    {
        [SerializeField] private string _id;
        public string Id => _id;
    }
}