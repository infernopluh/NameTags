using HarmonyLib;
using Photon.Pun;
using System.Collections.Generic;
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
            "Graze.WhoIsTalking",
        };

        void LateUpdate()
        {
            if (who.enabled)
            {
                toStare.transform.LookAt(GorillaTagger.Instance.headCollider.transform.position);
                text.text = GetPlayerFromVRRig(who).NickName;

                string modStatus = DetectPlayerMods(who);

                if (modStatus == "cheat")
                {
                    text.color = Color.red;
                }
                else if (modStatus == "legal")
                {
                    text.color = Color.blue;
                }
                else
                {
                    text.color = Color.white;
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

        private string DetectPlayerMods(VRRig p)
        {
            List<string> mods = GetPlayerMods(p);

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

        private List<string> GetPlayerMods(VRRig p)
        {
            return new List<string>();
        }
    }
}
