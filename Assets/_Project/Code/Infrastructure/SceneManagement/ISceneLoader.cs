using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Code.Infrastructure.SceneManagement
{
    public interface ISceneLoader
    {
        public UniTask Load(string nextScene, IProgress<float> progress = null, Action onLoaded = null);
    }
}
