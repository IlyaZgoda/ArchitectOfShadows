using UnityEngine;

namespace Code.StaticData.SceneManagement
{
    [CreateAssetMenu(fileName = "QuestData", menuName = "Static Data/Quest")]
    public class QuestStaticData : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private IQuest _initializer;
        [SerializeField] private ScriptableObject _cleanup;


        public string Name => _name;
        public IQuest Initializer => _initializer;
        public ScriptableObject Cleanup => _cleanup;
    }
}

