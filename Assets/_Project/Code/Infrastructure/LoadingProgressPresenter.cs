using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Code.Infrastructure
{
    public class LoadingProgressPresenter : IProgress<float>
    {
        public event Action<float> OnProgress;
        private const float _ratio = 0.9f;

        public void Report(float value) =>       
            OnProgress?.Invoke(Mathf.Clamp01(value / _ratio));      
    }
}
