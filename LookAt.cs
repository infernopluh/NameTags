using HarmonyLib;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace NameTags
{
    internal class LookAt : MonoBehaviour
    {
        public GameObject toStare = null;
        public UnityEngine.UI.Text text = null;
        public VRRig who = null;

        void LateUpdate()
        {
            if (who.enabled)
            {
                toStare.transform.LookAt(GorillaTagger.Instance.headCollider.transform.position);
                text.text = GetPlayerFromVRRig(who).NickName;
                text.color = who.mainSkin.material.name.Contains("fected") ? new Color(1f, 0.5f, 0f, 1) : who.playerColor;
            } else
            {
                text.text = "null";
            }
        }

        public NetPlayer GetPlayerFromVRRig(VRRig p)
        {
            //return GetPhotonViewFromVRRig(p).Owner;
            return p.Creator;
        }
    }
}
