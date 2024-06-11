using UnityEngine;

namespace Code.StaticData.SceneManagement
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "Static Data/Level")]
    public class LevelStaticData : ScriptableObject
    {
        [SerializeField] private string _levelKey;

        public string LevelKey => _levelKey;
    }
}

