using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CipherWheelAnimation : MonoBehaviour
{
    [SerializeField] private GameObject target;
    public float duration;
    public float startY;
    public float endY;

    // Start is called before the first frame update
    void Start()
    {
        RotationAnimation rotationAnimation = new RotationAnimation();
        bool smooth = true;

        PlayRotationAnimation(startY, endY, duration);
        // rotationAnimation.PlayRotationAnimation(startY, endY, duration, smooth, target);
    }

    void PlayRotationAnimation(float startY, float endY, float duration)
    {
        // Create a new AnimationClip
        AnimationClip clip = new AnimationClip();

        // Create an AnimationCurve for rotation.y
        AnimationCurve curve = AnimationCurve.EaseInOut(0, startY, duration, endY);

        // Assign curve to the AnimationClip
        clip.SetCurve("", typeof(Transform), "localRotation.eulerAngles.y", curve);

        // Add the AnimationClip to an Animation component
        Animation animation = target.AddComponent<Animation>();
        animation.clip = clip;

        // Play the animation
        animation.Play();
    }
}
