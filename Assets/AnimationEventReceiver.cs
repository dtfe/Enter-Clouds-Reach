using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using System;

namespace EnterCloudsReach.Animation
{
    public class AnimationEventReceiver : MonoBehaviour
    {
        public VisualEffect vfx;

        public void VFXStart()
        {
            vfx.Play();
        }
    }
}
