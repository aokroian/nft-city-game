using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMainController : MonoBehaviour
{
    public LoadSceneEvent loadSceneEvent;
    public LoadSceneEP gameSceneEP;

    public void OnPlay()
    {
        loadSceneEvent.Raise(gameSceneEP);
    }
}
