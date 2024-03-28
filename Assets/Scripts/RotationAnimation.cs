using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationAnimation : MonoBehaviour
{
    public AnimationCurve GetAnimationCurve(float startY, float endY, float duration, bool smoothInOut)
    {
        AnimationCurve curve = new AnimationCurve();

        // Add keyframes for start and end values
        curve.AddKey(0f, startY);
        curve.AddKey(duration, endY);

        // If smoothInOut is true, add tangents to make the animation ease in and out
        if (smoothInOut)
        {
            Keyframe startKey = curve[0];
            Keyframe endKey = curve[curve.length - 1];
            startKey.outTangent = 0;
            endKey.inTangent = 0;
            curve.MoveKey(0, startKey);
            curve.MoveKey(curve.length - 1, endKey);
        }

        return curve;
    }

    public AnimationClip CreateRotationAnimation(float startY, float endY, float duration, bool smoothInOut)
    {
        // Create a new AnimationClip
        AnimationClip clip = new AnimationClip();

        // Create an AnimationCurve for rotation.y
        AnimationCurve curve = GetAnimationCurve(startY, endY, duration, smoothInOut);

        // Assign curve to the AnimationClip
        clip.SetCurve("", typeof(Transform), "localRotation.eulerAngles.y", curve);

        // Return the AnimationClip
        return clip;
    }

    public void PlayRotationAnimation(float startY, float endY, float duration, bool smoothInOut, GameObject target)
    {
        // Create the AnimationClip
        AnimationClip rotationClip = CreateRotationAnimation(startY, endY, duration, smoothInOut);

        // Create an Animation component
        Animation animation = target.AddComponent<Animation>();

        // Add the AnimationClip to the Animation component
        animation.clip = rotationClip;

        // Play the animation
        animation.Play();
    }
}

