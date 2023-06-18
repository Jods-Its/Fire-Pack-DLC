using UnityEngine;
using HarmonyLib;
using Il2Cpp;

namespace FirePack
{
    internal class TakeEmbersButton
    {
        internal static string embersText;
        private static GameObject embersButton;         

        internal static System.Action GetActionDelegate() => new System.Action(OnTakingEmbers);

        internal static void Initialize(Panel_FeedFire panel_FeedFire)
        {
            if (panel_FeedFire == null) return;

            embersText = Localization.Get("GAMEPLAY_TakeEmbers");

            embersButton = GameObject.Instantiate<GameObject>(panel_FeedFire.m_ActionButtonObject, panel_FeedFire.m_ActionButtonObject.transform.parent, true);
            embersButton.transform.Translate(0, 0.09f, 0);
            Utils.GetComponentInChildren<UILabel>(embersButton).text = embersText;
            AddAction(embersButton, new System.Action(OnTakingEmbers));

            NGUITools.SetActive(embersButton, true);
        }
        private static void AddAction(GameObject button, System.Action action)
        {
            Il2CppSystem.Collections.Generic.List<EventDelegate> placeHolderList = new Il2CppSystem.Collections.Generic.List<EventDelegate>();
            placeHolderList.Add(new EventDelegate(action));
            Utils.GetComponentInChildren<UIButton>(button).onClick = placeHolderList;
        }
        internal static void SetActive(bool active)
        {
            NGUITools.SetActive(embersButton, active);
        }
        internal static void OnTakingEmbers()
        {
            //Fire thisFire = InterfaceManager.GetPanel<Panel_FeedFire>().GetComponentInChildren<Fire>();
            FireUtils.TakeEmbers();
        }
    }

    [HarmonyPatch(typeof(Panel_FeedFire), "Initialize")]
    internal class Panel_FeedFire_Initialize
    {
        private static void Postfix(Panel_FeedFire __instance)
        {
            //MelonLoader.MelonLogger.Msg("FeedFire_Initialize");
            TakeEmbersButton.Initialize(__instance);
        }
    } 

    [HarmonyPatch(typeof(Panel_FeedFire), "Enable")]
    internal class Panel_FeedFire_Enable
    {
        private static void Postfix(bool enable)
        {
            //MelonLoader.MelonLogger.Msg("FeedFire_Enable");
            if (!enable) return;
            if (FireUtils.HasEmberBox()) TakeEmbersButton.SetActive(true);
            else TakeEmbersButton.SetActive(false);
        }
    }
}
