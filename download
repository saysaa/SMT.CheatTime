using BepInEx;
using UnityEngine;
using System.Reflection;
using System;

namespace SMT.CheatTime
{
    [BepInPlugin("com.marcon.smt_trainer", "SMT CheatTime", "1.2")]
    public class TrainerPlugin : BaseUnityPlugin
    {
        private bool showMenu = true;
        private bool noClip = false;
        private string luckyMessage = "";
        private float messageTimer = 0f;
        private Vector2 scrollPos = Vector2.zero;

        private float speedValue = 1.0f;
        private int currentTab = 0;
        private bool unlockedLucky = false;

        private void Awake() => Logger.LogInfo("==== >_ SMT.CheatTime! | F3 to toggle menu ====");

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F3))
            {
                showMenu = !showMenu;
                Cursor.visible = showMenu;
                Cursor.lockState = showMenu ? CursorLockMode.None : CursorLockMode.Locked;
            }

            if (messageTimer > 0)
            {
                messageTimer -= Time.deltaTime;
                if (messageTimer <= 0) luckyMessage = "";
            }

            if (noClip)
            {
                var p = GameObject.FindAnyObjectByType<PlayerObjectController>();
                var cam = Camera.main;
                if (p != null && cam != null)
                {
                    Vector3 dir = Vector3.zero;
                    if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Z)) dir += cam.transform.forward;
                    if (Input.GetKey(KeyCode.S)) dir -= cam.transform.forward;
                    if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.Q)) dir -= cam.transform.right;
                    if (Input.GetKey(KeyCode.D)) dir += cam.transform.right;

                    dir.y = 0;

                    if (dir != Vector3.zero)
                    {
                        p.transform.position += dir.normalized * (0.15f * speedValue);
                    }
                }
            }
        }

        private void SetAllCharactersScale(float scale)
        {
            var players = GameObject.FindObjectsByType<PlayerObjectController>(FindObjectsSortMode.None);
            foreach (var p in players)
            {
                p.transform.localScale = new Vector3(scale, scale, scale);
            }
        }

        private void LuckyCheats()
        {
            System.Random rng = new System.Random();

            int maxChoices = unlockedLucky ? 5 : 4;
            int choice = rng.Next(0, maxChoices);

            messageTimer = 1.5f;

            switch (choice)
            {
                case 0:
                    if (GameData.Instance != null) GameData.Instance.NetworkgameFunds += 10000000f;
                    luckyMessage = "+10000000 $";
                    break;
                case 1:
                    if (GameData.Instance != null) GameData.Instance.NetworkgameFunds = 0f;
                    luckyMessage = "nooooo bye bye money";
                    break;
                case 2:
                    SetAllCharactersScale(3.5f);
                    luckyMessage = "Giant Mode !";
                    break;
                case 3:
                    SetAllCharactersScale(0.5f);
                    luckyMessage = "Tiny Mode !";
                    break;
                case 4:
                    luckyMessage = "boummmm ahhhhhhh";
                    System.Diagnostics.Process.GetCurrentProcess().Kill();
                    break;
            }
        }

        private void ToggleNoClip()
        {
            noClip = !noClip;
            var p = GameObject.FindAnyObjectByType<PlayerObjectController>();
            if (p != null)
            {
                Component col = p.GetComponent("Collider");
                if (col != null)
                {
                    PropertyInfo prop = col.GetType().GetProperty("enabled");
                    if (prop != null) prop.SetValue(col, !noClip, null);
                }

                Component rb = p.GetComponent("Rigidbody");
                if (rb != null)
                {
                    PropertyInfo gravProp = rb.GetType().GetProperty("useGravity");
                    if (gravProp != null) gravProp.SetValue(rb, !noClip, null);
                }
            }
        }

        private void OnGUI()
        {
            if (!showMenu) return;

            if (messageTimer > 0)
            {
                GUIStyle style = new GUIStyle();
                style.fontSize = 24;
                style.normal.textColor = Color.yellow;
                style.alignment = TextAnchor.MiddleCenter;
                GUI.Label(new Rect(0, Screen.height / 4, Screen.width, 50), luckyMessage, style);
            }

            float x = Screen.width - 290f;
            float y = 100f;
            float boxWidth = 250f;
            float boxHeight = 350f;

            GUI.Box(new Rect(x, y, boxWidth, boxHeight), "SMT.CheatTime! 1.2");

            if (GUI.Button(new Rect(x + 10, y + 30, 110, 25), "Cheats")) currentTab = 0;
            if (GUI.Button(new Rect(x + 130, y + 30, 110, 25), "Settings")) currentTab = 1;

            scrollPos = GUI.BeginScrollView(new Rect(x + 10, y + 60, boxWidth - 20, boxHeight - 70),
                                            scrollPos, new Rect(0, 0, boxWidth - 40, 500));

            switch (currentTab)
            {
                case 0: //cheats
                    if (GUI.Button(new Rect(0, 0, 210, 35), "[+] | 1M Money"))
                        GameData.Instance.NetworkgameFunds += 1000000f;

                    string ncLabel = noClip ? "[+] | No-Clip (ON)" : "[-] | No-Clip (OFF)";
                    if (GUI.Button(new Rect(0, 45, 210, 35), ncLabel))
                        ToggleNoClip();

                    if (GUI.Button(new Rect(0, 90, 210, 35), "[+] | 10 Franchise Points"))
                        GameData.Instance.NetworkgameFranchisePoints += 10;

                    if (unlockedLucky)
                    {
                        if (GUI.Button(new Rect(0, 135, 210, 35), "[?] | I am Lucky ? [ON]"))
                            LuckyCheats();
                    }
                    else
                    {
                        GUI.enabled = false;
                        GUI.Button(new Rect(0, 135, 210, 35), "[?] | I am Lucky ? [OFF]");
                        GUI.enabled = true;
                    }

                    GUI.Label(new Rect(0, 180, 210, 20), "[+] | SpeedHack : " + speedValue.ToString("F1") + "x");
                    speedValue = GUI.HorizontalSlider(new Rect(0, 205, 210, 15), speedValue, 1.0f, 10.0f);
                    Time.timeScale = speedValue;
                    break;

                case 1: //settings
                    string unlock = unlockedLucky ? "I am Lucky mod [ON]" : "I am Lucky mod [OFF]";
                    if (GUI.Button(new Rect(0, 0, 210, 35), unlock))
                    {
                        unlockedLucky = !unlockedLucky;
                    }

                    if (GUI.Button(new Rect(0, 40, 210, 35), "SMT.CheatTime Github Repo"))
                        System.Diagnostics.Process.Start("https://github.com/saysaa/SMT.CheatTime");
                    break;
            }

            GUI.EndScrollView();
        }
    }
}