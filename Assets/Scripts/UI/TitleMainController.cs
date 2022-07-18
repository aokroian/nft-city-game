using UnityEngine;

namespace UI
{
    public class TitleMainController : MonoBehaviour
    {
        public LoadSceneEvent loadSceneEvent;
        public LoadSceneEP gameSceneEP;

        public void OnPlay()
        {
            loadSceneEvent.Raise(gameSceneEP);
        }
    }
}
