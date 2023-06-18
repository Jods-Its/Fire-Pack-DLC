using Il2Cpp;
using HarmonyLib;
//using Il2CppList = Il2CppSystem.Collections.Generic.List<Panel_ActionPicker.ActionPickerItemData>;
//using SystemList = System.Collections.Generic.List<Panel_ActionPicker.ActionPickerItemData>;

namespace FirePack.OldCode
{
    /*
      private List<ActionPickerItemData> 
      internal class EmbersActionPickerButton
      {
          internal static void Execute(Fire thisFire)
          {
              FireUtils.TakeEmbers();
          }
      }

      [HarmonyPatch(typeof(Panel_ActionPicker), "EnableWithCurrentList")]
      internal class Panel_ActionPicker_ShowCustomActionPicker
      {
          internal static void Prefix(Panel_ActionPicker __instance)
          {
              if (!FireUtils.IsBurningFire(__instance.m_ObjectInteractedWith) || !FireUtils.HasEmberBox()) return;

              SystemList replacement = FireUtils.Convert<ActionPickerItemData>(__instance.m_ActionPickerItemDataList);
              Action act = new Action(() => FireUtils.TakeEmbers(__instance.m_ObjectInteractedWith.GetComponent<Fire>()));
              replacement.Insert(2, new ActionPickerItemData("ico_skills_fireStarting", "GAMEPLAY_TakeEmbers", act));
              ReplaceList(__instance.m_ActionPickerItemDataList, replacement);
          }
          private static void ReplaceList(Il2CppList original, SystemList actionList)
          {
              if (original is null || actionList is null) return;
              original.Clear();
              foreach (var element in actionList) original.Add(element);
          }
      }
      */
}
