using BepInEx;
using System;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace NameTags
{
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        void Update()
        {
            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
            {
                if (vrrig != GorillaTagger.Instance.offlineVRRig && !vrrig.headMesh.transform.Find("NameTag(Clone)"))
                {
                    GameObject literallynametag = LoadAsset("NameTag");
                    print(literallynametag.name);
                    literallynametag.transform.SetParent(vrrig.headMesh.transform, false);
                    LookAt a = literallynametag.AddComponent<LookAt>();
                    a.toStare = literallynametag.transform.Find("Canvas").gameObject;
                    a.text = a.toStare.transform.Find("Name").gameObject.GetComponent<UnityEngine.UI.Text>();
                    a.who = vrrig;
                }
            }
        }

        static AssetBundle assetBundle = null;
        public static GameObject LoadAsset(string assetName)
        {
            GameObject gameObject = null;

            if (assetBundle == null)
            {
                Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("NameTags.Resources.nametag");
                assetBundle = AssetBundle.LoadFromStream(stream);
                stream.Close();
            }
            gameObject = Instantiate<GameObject>(assetBundle.LoadAsset<GameObject>(assetName));

            return gameObject;
        }
    }
}
