using HarmonyLib;
using Photon.Pun;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace NameTags
{
    internal class LookAt : MonoBehaviour
    {
        public GameObject toStare = null;
        public UnityEngine.UI.Text text = null;
        public VRRig who = null;

        private static readonly HashSet<string> CheatMods = new HashSet<string>
        {
            "org.iidk.gorillatag.iimenu",
      
        };

        private static readonly HashSet<string> LegalMods = new HashSet<string>
        {
        
        };

        void LateUpdate()
        {
            if (who.enabled)
            {
                toStare.transform.LookAt(GorillaTagger.Instance.headCollider.transform.position);
                text.text = GetPlayerFromVRRig(who).NickName;

                string modStatus = DetectPlayerMods();

                if (modStatus == "cheat")
                {
                    text.color = Color.red; // Cheat detected
                }
                else if (modStatus == "legal")
                {
                    text.color = Color.blue; // Legal mods detected
                }
                else
                {
                    text.color = Color.white; // No mods detected
                }
            }
            else
            {
                text.text = "null";
            }
        }

        public NetPlayer GetPlayerFromVRRig(VRRig p)
        {
            return p.Creator;
        }

        private string DetectPlayerMods()
        {
            List<string> mods = GetPlayerMods();

            if (mods.Count == 0) return "none";

            foreach (string mod in mods)
            {
                if (CheatMods.Contains(mod)) return "cheat";
            }

            foreach (string mod in mods)
            {
                if (LegalMods.Contains(mod)) return "legal";
            }

            return "none";
        }

        private List<string> GetPlayerMods()
        {
            string pluginsPath = Path.Combine(Application.dataPath, "../BepInEx/plugins");
            List<string> mods = new List<string>();

            if (Directory.Exists(pluginsPath))
            {
                string[] pluginFiles = Directory.GetFiles(pluginsPath, "*.dll");
                foreach (string pluginFile in pluginFiles)
                {
                    mods.Add(Path.GetFileNameWithoutExtension(pluginFile));
                }
            }

            return mods;
        }
    }
}
