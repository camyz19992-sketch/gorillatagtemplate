using UnityEngine;
using YourMenuName.Classes;

namespace YourMenuName.Classes
{
    public class Button : MonoBehaviour
    {
        public ButtonInfo info;

        void OnTriggerEnter(Collider other)
        {
            if (other.name.Contains("Hand") || other.name.Contains("finger"))
            {
                if (info.isToggle)
                {
                    info.enabled = !info.enabled;
                    GetComponent<Renderer>().material.color = info.enabled ? 
                        Menu.Settings.enabledColor : Menu.Settings.buttonColor;
                }
                else
                {
                    info.method?.Invoke();
                }
            }
        }
    }
}