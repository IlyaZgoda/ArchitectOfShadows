using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Code.Infrastructure
{
    public class CoroutineRunner : MonoBehaviour, ICoroutineRunner
    {
        public void Awake()      
            => DontDestroyOnLoad(this);       
    }
}
