using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreRestorer : MonoBehaviour
{
    [SerializeField] List<CorePlatformDisappear> corePlatformsToRestore;
    [SerializeField] CorruptedArchitect corruptedArchitect;
    [SerializeField] BossAltar bossAltar;

    public void RestoreCore()
    {
        foreach(CorePlatformDisappear platform in  corePlatformsToRestore)
        {
            platform.RestoreToOriginPosition();
        }

        corruptedArchitect.ResetAll();
        if(bossAltar.isActiveAndEnabled) bossAltar.ResetAll();
    }
}
