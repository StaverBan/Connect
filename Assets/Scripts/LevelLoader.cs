using UnityEngine;

namespace DefaultNamespace
{
    public class LevelLoader: MonoBehaviour
    {
        [SerializeField] private LevelContainer _levelContainer;

        private GameObject nowLevelGameObject;
        
        
        public LevelSettings LoadLevel(int nowLevel)
        {
            DestroyLevel();
            
            var level = _levelContainer.LevelData[nowLevel];
            var levelObject = Object.Instantiate(level, Vector3.zero, Quaternion.identity);

            nowLevelGameObject = levelObject.gameObject;
            return levelObject.GetComponent<LevelSettings>();
        }

        private void DestroyLevel()
        {
            if(nowLevelGameObject)
                Object.Destroy(nowLevelGameObject);
        }
        
    }
}