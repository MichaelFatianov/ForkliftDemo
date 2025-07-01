using System.Collections.Generic;
using UnityEngine;

namespace Common.Engine
{
    [CreateAssetMenu(fileName = "EnginesDatabase", menuName = "Data/EnginesDatabase")]
    public class EngineDatabase : Database
    {
        [SerializeField] private EngineData[] _enginesData;
        private Dictionary<string, EngineData> _mappedEnginesData;

        public Dictionary<string, EngineData> MappedEnginesData
        {
            get
            {
                if (_mappedEnginesData != null && _mappedEnginesData.Count != 0) return _mappedEnginesData;

                _mappedEnginesData = new Dictionary<string, EngineData>();

                foreach (var engineData in _enginesData) _mappedEnginesData.Add(engineData.Id, engineData);

                return _mappedEnginesData;
            }
        }
    }
}