using System;

namespace YourMenuName.Classes
{
    public class ButtonInfo
    {
        public string buttonText;
        public bool enabled;
        public Action method;
        public string toolTip;
        public bool isToggle = true; // true = toggle, false = one-shot

        public ButtonInfo(string text, Action method, string tip = "", bool toggle = true)
        {
            buttonText = text;
            this.method = method;
            toolTip = tip;
            isToggle = toggle;
            enabled = false;
        }
    }
}