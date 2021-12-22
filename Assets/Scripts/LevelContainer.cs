using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(order = 0, fileName = "LevelContainer", menuName = "LevelContainer/Config")]
    public class LevelContainer: ScriptableObject
    {
        public List<LevelSettings> LevelData;
    }
}