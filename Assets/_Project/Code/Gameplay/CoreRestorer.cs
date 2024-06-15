using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreRestorer : MonoBehaviour
{
    [SerializeField] List<CorePlatformDisappear> corePlatformsToRestore;

    public void RestoreCore()
    {
        foreach(CorePlatformDisappear platform in  corePlatformsToRestore)
        {
            platform.RestoreToOriginPosition();
        }
    }
}
