using Il2Cpp;
using Il2CppTLD.Gear;
using HarmonyLib;
using Il2CppNodeCanvas.Framework;
using static UnityEngine.UI.Image;
using UnityEngine;
using System.Linq;

namespace FirePack
{
    internal class Patches
    {
        [HarmonyPatch(typeof(Panel_FeedFire), "CanTakeTorch")]
        [HarmonyPriority(Priority.Last)]
        internal class Panel_FeedFire_CanTakeTorch
        {
            private static void Postfix(ref bool __result)
            {
                if (!Settings.instance.pullTorches) __result = false;
            }
        }

        [HarmonyPatch(typeof(Fire), "CanTakeTorch")]
        [HarmonyPriority(Priority.Last)]
        internal class Fire_CanTakeTorch
        {
            private static void Postfix(ref bool __result)
            {
                if (!Settings.instance.pullTorches) __result = false;
            }
        }

        [HarmonyPatch(typeof(FireManager), "PlayerStartFire")]
        internal class FireManager_PlayerStartFire
        {
            private static void Prefix(FireStarterItem starter)
            {
                if (Settings.instance.consumeTorchOnFirestart && starter.name.StartsWith("GEAR_Torch"))
                {
                    starter.m_ConditionDegradeOnUse = 100;
                    starter.m_ConsumeOnUse = true;
                }
            }
        }

        [HarmonyPatch(typeof(StartGear), "AddAllToInventory")]
        internal class StartGear_AddAllToInventory
        {
            private static void Postfix()
            {
                if (Settings.instance.noWoodMatches) GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(FireUtils.matches, 20);
            }
        }

        [HarmonyPatch(typeof(GearItem), "Awake")]
        internal class DestroyWoodMatches
        {
            private static void Postfix(GearItem __instance)
            {
                if (Settings.instance.noWoodMatches && __instance.name.Replace("(Clone)", "") == "GEAR_WoodMatches")
                {
                    UnityEngine.Object.Destroy(__instance.gameObject);
                }
            }
        }
        [HarmonyPatch(typeof(Panel_ActionPicker), "EnableWithCurrentList")]
        internal class Panel_ActionPicker_EnableWithCurrentList
        {
            private static void Prefix(Panel_ActionPicker __instance)
            {
                if (!FireUtils.IsBurningFire(__instance.m_ObjectInteractedWith) || !FireUtils.HasEmberBox())
                {
                    return;
                }
                List<ActionPickerItemData> replacement = FireUtils.Convert<ActionPickerItemData>(__instance.m_ActionPickerItemDataList);
                Action act = new Action(() => FireUtils.TakeEmbers(__instance.m_ObjectInteractedWith.GetComponent<Fire>()));
                replacement.Insert(2, new ActionPickerItemData("ico_skills_fireStarting", "GAMEPLAY_TakeEmbers", act));
                __instance.m_ActionPickerItemDataList = FireUtils.Convert(replacement);
                if(__instance.m_ActionPickerItemList.Count < __instance.m_ActionPickerItemDataList.Count)
                {
                    int HowMuchNeed = __instance.m_ActionPickerItemList.Count- __instance.m_ActionPickerItemList.Count;
                    GameObject Doner = __instance.m_ActionPickerItemList[__instance.m_ActionPickerItemList.Count - 1].gameObject;
                    for (int i = 0; i < HowMuchNeed; i++)
                    {
                        GameObject Clone = GameObject.Instantiate<GameObject>(Doner, Doner.transform.parent, true);
                        if(Clone != null)
                        {
                            __instance.m_ActionPickerItemList.AddItem(Clone.GetComponent<ActionPickerItem>());
                        }
                    }
                }
            }
        }
        // InteractiveObjectsProcessInteraction is method that supposed to trigger once you try to click on object.
        [HarmonyPatch(typeof(PlayerManager), "InteractiveObjectsProcessInteraction")]
        internal class PlayerManager_InteractiveObjectsProcessInteraction
        {
            private static void Postfix(PlayerManager __instance)
            {
                // Cause this method does not return and does not store (at least I dont know where is it)
                // We gotta find object we are looking at (well at least in most situations that what object we going to interact with)
                float maxRange = __instance.ComputeModifiedPickupRange(GameManager.GetGlobalParameters().m_MaxPickupRange);
                if (GameManager.GetPlayerManagerComponent().GetControlMode() == PlayerControlMode.InFPCinematic)
                {
                    maxRange = 50f;
                }
                GameObject GO = __instance.GetInteractiveObjectNearCrosshairs(maxRange);
                // Found object
                if (GO != null)
                {
                    Fire F = GO.GetComponent<Fire>();
                    // Object got fire.
                    if (F != null)
                    {
                        // Storing for future use.
                        FireUtils.LastInteractedFire = F;
                        return;
                    } else
                    {
                        Campfire CF = GO.GetComponent<Campfire>();
                        if(CF != null)
                        {
                            FireUtils.LastInteractedFire = CF.Fire;
                            return;
                        }
                        WoodStove WS = GO.GetComponent<WoodStove>();
                        if(WS != null)
                        {
                            FireUtils.LastInteractedFire = WS.Fire;
                            return;
                        }
                    }
                }
            }
        }
    }
}
