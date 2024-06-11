using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;

namespace Code.Infrastructure.SceneManagement
{
    public class SceneLoader : ISceneLoader
    {
        public async UniTask Load(string nextScene, IProgress<float> progress = null, Action onLoaded = null)
        {
            if(SceneManager.GetActiveScene().name == nextScene)
            {
                onLoaded?.Invoke();
                return;
            }

            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextScene);
            waitNextScene.allowSceneActivation = false;

            while (!waitNextScene.isDone)
            {
                progress?.Report(waitNextScene.progress);

                if (waitNextScene.progress >= 0.9f)
                    waitNextScene.allowSceneActivation = true;

                await UniTask.Yield();
            }

            onLoaded?.Invoke();
        }     
    }
}
