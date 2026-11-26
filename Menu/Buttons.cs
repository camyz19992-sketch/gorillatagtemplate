using System.Collections.Generic;
using YourMenuName.Classes;
using YourMenuName.Mods;

namespace YourMenuName.Menu
{
    public static class Buttons
    {
        public static List<ButtonInfo[]> buttons = new List<ButtonInfo[]>
        {
            // Page 0
            new ButtonInfo[]
            {
                new ButtonInfo("Speed Boost", () => Movement.SpeedBoost(), "Increases movement speed"),
                new ButtonInfo("Fly", () => Movement.Fly(), "Hold trigger to fly"),
                new ButtonInfo("No Clip", () => Movement.NoClip(), "Walk through walls"),
                new ButtonInfo("Platforms", () => Movement.Platforms(), "Spawn platforms under hands"),
            },
            // Page 1 - add more pages as needed
            new ButtonInfo[]
            {
                new ButtonInfo("Example Mod", () => { /* your code */ }, "Tooltip here"),
            }
        };
    }
}