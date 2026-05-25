using BepInEx;
using UnityEngine;
using System.Reflection;

namespace SMT.CheatTime
{
    [BepInPlugin("com.marcon.smt_trainer", "SMT CheatTime", "1.0")]
    public class TrainerPlugin : BaseUnityPlugin
    {
        private bool showMenu = true;
        private bool noClip = false;

        private void Awake() => Logger.LogInfo("==== >_ CheatTime! | F1 to toggle menu ====");

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F1)) showMenu = !showMenu;
            if (Input.GetKeyDown(KeyCode.F2) && GameData.Instance != null) GameData.Instance.NetworkgameFunds = (GameData.Instance.gameFunds += 1000000f);

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
                        p.transform.position += dir.normalized * 0.15f;
                }
            }
        }

        private void OnGUI()
        {
            if (!showMenu) return;

            float x = Screen.width - 290f; float y = Screen.height - 245f;
            GUI.Box(new Rect(x, y, 250, 160), "SMT.CheatTime! | v1.0");

            if (GUI.Button(new Rect(x + 10, y + 30, 230, 35), "[+] | 1 000 000 $") && GameData.Instance != null)
                GameData.Instance.NetworkgameFunds = (GameData.Instance.gameFunds += 1000000f);

            if (GUI.Button(new Rect(x + 10, y + 75, 230, 35), "[+] | 10 Franchise Points") && GameData.Instance != null)
                GameData.Instance.NetworkgameFranchisePoints = (GameData.Instance.gameFranchisePoints += 10);

            string ncText = noClip ? "[+] | No-Clip" : "[-] | No-Clip";
            if (GUI.Button(new Rect(x + 10, y + 120, 230, 35), ncText))
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
        }
    }
}