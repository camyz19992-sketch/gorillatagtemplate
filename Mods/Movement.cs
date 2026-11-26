using GorillaLocomotion;
using UnityEngine;

namespace YourMenuName.Mods
{
    public static class Movement
    {
        public static void SpeedBoost()
        {
            // Simple example - improve this
            if (GTPlayer.Instance != null)
            {
                GTPlayer.Instance.maxJumpSpeed = 9f;
                GTPlayer.Instance.jumpMultiplier = 1.5f;
            }
        }

        public static void Fly()
        {
            // Basic fly example (hold right trigger)
            if (ControllerInputPoller.instance.rightControllerIndexFloat > 0.5f)
            {
                GTPlayer.Instance.transform.position += 
                    GTPlayer.Instance.headCollider.transform.forward * Time.deltaTime * 12f;
            }
        }

        public static void NoClip()
        {
            // Very basic - you will want a proper implementation
            foreach (var col in GTPlayer.Instance.GetComponentsInChildren<Collider>())
            {
                // toggle logic would go here
            }
        }

        public static void Platforms()
        {
            // Placeholder - implement platform spawning
        }
    }
}