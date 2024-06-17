using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolDownTime : MonoBehaviour
{
    public static float Cooldown(float cooldown)
    {
        return Time.time + cooldown;
    }
}
