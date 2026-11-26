using HarmonyLib;
using GorillaLocomotion;
using UnityEngine;
using UnityEngine.UI;
using YourMenuName.Classes;
using static YourMenuName.Menu.Buttons;
using static YourMenuName.Menu.Settings;

namespace YourMenuName.Menu
{
    [HarmonyPatch(typeof(GTPlayer), "LateUpdate")]
    public class Main
    {
        public static GameObject menu;
        public static GameObject menuBackground;
        public static GameObject canvasObject;
        public static int pageNumber = 0;
        public static GameObject reference;

        public static void Prefix()
        {
            bool toOpen = (!rightHanded && ControllerInputPoller.instance.leftControllerSecondaryButton) ||
                          (rightHanded && ControllerInputPoller.instance.rightControllerSecondaryButton);
            bool keyboardOpen = Input.GetKey(keyboardButton);

            if (menu == null)
            {
                if (toOpen || keyboardOpen)
                {
                    CreateMenu();
                    RecenterMenu(rightHanded, keyboardOpen);
                }
            }
            else
            {
                if (toOpen || keyboardOpen)
                    RecenterMenu(rightHanded, keyboardOpen);
                else
                {
                    Object.Destroy(menu);
                    menu = null;
                    if (reference != null) Object.Destroy(reference);
                }
            }

            // Run enabled mods every frame
            foreach (var page in buttons)
            {
                foreach (var btn in page)
                {
                    if (btn.enabled && btn.method != null)
                        btn.method.Invoke();
                }
            }
        }

        public static void CreateMenu()
        {
            menu = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Object.Destroy(menu.GetComponent<Rigidbody>());
            Object.Destroy(menu.GetComponent<BoxCollider>());
            Object.Destroy(menu.GetComponent<Renderer>());
            menu.transform.localScale = new Vector3(0.1f, 0.3f, 0.3825f);

            // Background
            menuBackground = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Object.Destroy(menuBackground.GetComponent<Rigidbody>());
            Object.Destroy(menuBackground.GetComponent<BoxCollider>());
            menuBackground.transform.parent = menu.transform;
            menuBackground.transform.localScale = menuSize;
            menuBackground.GetComponent<Renderer>().material.color = backgroundColor;
            menuBackground.transform.localPosition = new Vector3(0.05f, 0f, 0f);

            // Canvas
            canvasObject = new GameObject("MenuCanvas");
            canvasObject.transform.parent = menu.transform;
            var canvas = canvasObject.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.WorldSpace;
            canvasObject.AddComponent<CanvasScaler>().dynamicPixelsPerUnit = 1000f;
            canvasObject.AddComponent<GraphicRaycaster>();

            // Title
            CreateText(PluginInfo.Name + $" [{pageNumber + 1}]", new Vector3(0.06f, 0f, 0.16f), 0.04f);

            // Buttons for current page
            if (pageNumber < buttons.Count)
            {
                float yStart = 0.12f;
                float spacing = 0.035f;

                for (int i = 0; i < buttons[pageNumber].Length && i < buttonsPerPage; i++)
                {
                    var btnInfo = buttons[pageNumber][i];
                    CreateButton(btnInfo, new Vector3(0.06f, 0f, yStart - (i * spacing)));
                }
            }

            // Page buttons (simple)
            CreatePageButton("<", new Vector3(0.06f, 0.22f, -0.15f), () => { if (pageNumber > 0) pageNumber--; Refresh(); });
            CreatePageButton(">", new Vector3(0.06f, -0.22f, -0.15f), () => { if (pageNumber < buttons.Count - 1) pageNumber++; Refresh(); });
        }

        static void CreateButton(ButtonInfo info, Vector3 pos)
        {
            var btn = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Object.Destroy(btn.GetComponent<Rigidbody>());
            btn.GetComponent<BoxCollider>().isTrigger = true;
            btn.transform.parent = menu.transform;
            btn.transform.localScale = new Vector3(0.09f, 0.8f, 0.08f);
            btn.transform.localPosition = pos;
            btn.GetComponent<Renderer>().material.color = info.enabled ? enabledColor : buttonColor;

            var buttonComp = btn.AddComponent<Button>();
            buttonComp.info = info;

            CreateText(info.buttonText, pos + new Vector3(0.01f, 0, 0), 0.025f);
        }

        static void CreatePageButton(string text, Vector3 pos, System.Action onClick)
        {
            // Simplified page button
            var btn = GameObject.CreatePrimitive(PrimitiveType.Cube);
            // ... similar setup, attach a simple click handler
        }

        static void CreateText(string content, Vector3 pos, float size)
        {
            var go = new GameObject();
            go.transform.parent = canvasObject.transform;
            var t = go.AddComponent<Text>();
            t.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            t.text = content;
            t.fontSize = 1;
            t.color = textColor;
            t.alignment = TextAnchor.MiddleCenter;
            t.resizeTextForBestFit = true;
            var rt = t.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(0.25f, size);
            rt.localPosition = pos;
            rt.rotation = Quaternion.Euler(180, 90, 90);
        }

        public static void RecenterMenu(bool rightHand, bool keyboard)
        {
            if (menu == null) return;

            Transform hand = rightHand ? GTPlayer.Instance.RightHand.transform : GTPlayer.Instance.LeftHand.transform;
            menu.transform.position = hand.position;
            menu.transform.rotation = hand.rotation;
        }

        static void Refresh()
        {
            if (menu != null)
            {
                Object.Destroy(menu);
                menu = null;
                CreateMenu();
            }
        }
    }
}