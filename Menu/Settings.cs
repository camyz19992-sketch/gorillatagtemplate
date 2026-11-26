using UnityEngine;

namespace YourMenuName.Menu
{
    public static class Settings
    {
        public static bool rightHanded = false;
        public static KeyCode keyboardButton = KeyCode.Q;

        // Colors
        public static Color backgroundColor = new Color(0.1f, 0.1f, 0.15f, 0.9f);
        public static Color buttonColor = new Color(0.2f, 0.2f, 0.3f);
        public static Color enabledColor = new Color(0.2f, 0.6f, 1f);
        public static Color textColor = Color.white;

        public static Vector3 menuSize = new Vector3(0.1f, 0.3f, 0.4f);
        public static int buttonsPerPage = 8;
    }
}