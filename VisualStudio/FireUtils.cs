using Il2Cpp;
using Il2CppRewired;
using Il2CppTLD.UI;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace FirePack
{
    internal static class FireUtils
    {
        public static GearItem matches = Addressables.LoadAssetAsync<GameObject>("GEAR_PackMatches").WaitForCompletion().GetComponent<GearItem>();
        public static GearItem activeEmberBox = Addressables.LoadAssetAsync<GameObject>("GEAR_ActiveEmberBox").WaitForCompletion().GetComponent<GearItem>();
		public static Fire LastInteractedFire = null;

		public static GameObject GetPlayer()
		{
			return GameManager.GetPlayerObject();
        }

        internal static bool HasEmberBox()
		{
			GearItem emberBox = GameManager.GetInventoryComponent().GetBestGearItemWithName("GEAR_EmberBox");
			if (emberBox is null || emberBox.IsWornOut()) return false;
			else return true;
		}

		internal static bool IsBurningFire(GameObject gameObject)
		{
			if (gameObject is null) return false;

			Fire componentInChildren = gameObject.GetComponentInChildren<Fire>();
			if (componentInChildren && componentInChildren.IsBurning()) return true;
			else return false;
		}

		internal static void TakeEmbers(Fire Fire)
		{
			// If we got nothing here, trying to grab from last interacted.
			if(Fire == null && LastInteractedFire != null)
			{
				Fire = LastInteractedFire;
            }

			if(Fire != null)
			{
                if (!Fire.m_IsPerpetual)
                {
                    Fire.ReduceHeatByDegrees(1);
                }
			}
			GearItem emberBox = GameManager.GetInventoryComponent().GetBestGearItemWithName("GEAR_EmberBox");
			if (emberBox == null)
			{
				GameAudioManager.PlayGUIError();
				HUDMessage.AddMessage(Localization.Get("GAMEPLAY_ToolRequiredToForceOpen").Replace("{item-name}", Localization.Get("GAMEPLAY_EmberBox")), false);
				return;
			}

			GameManager.GetInventoryComponent().DestroyGear(emberBox.gameObject);
			GearItem _activeEmberBox = GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(activeEmberBox, 1);
            GearMessage.AddMessage(_activeEmberBox, Localization.Get("GAMEPLAY_Harvested"), _activeEmberBox.DisplayName, false);
			GameObject Player = GetPlayer();
			if(Player != null)
			{
                GameAudioManager.PlaySound("Play_SndInvStoneSmall", Player);
                GameAudioManager.PlaySound("Play_MatchBurnOut", Player);
                GameAudioManager.PlaySound("Play_TinCanPutDown", Player);
            }

			InterfaceManager.GetPanel<Panel_FeedFire>().ExitFeedFireInterface();
        }

		internal static System.Collections.Generic.List<T> Convert<T>(Il2CppSystem.Collections.Generic.List<T> list)
		{
			System.Collections.Generic.List<T> result = new System.Collections.Generic.List<T>(list.Count);
			foreach (var element in list)
			{
				result.Add(element);
			}
			return result;
		}

		internal static Il2CppSystem.Collections.Generic.List<T> Convert<T>(System.Collections.Generic.List<T> list)
		{
			Il2CppSystem.Collections.Generic.List<T> result = new Il2CppSystem.Collections.Generic.List<T>(list.Count);
			foreach (var element in list)
			{
				result.Add(element);
			}
			return result;
		}

    }
}
