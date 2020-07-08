using BattleTech;
using BattleTech.DataObjects;
using Harmony;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTCommanderAbilities
{
    [HarmonyPatch(typeof(SimGameState), "Rehydrate")]
    class SimGameState_Rehydrate
    {
        public static void Postfix(SimGameState __instance)
        {
            if (__instance != null)
            {
                foreach (Pilot pilot in __instance.PilotRoster)
                {
                    HandlePilot(pilot, __instance);
                }
                HandlePilot(__instance.Commander, __instance);
            }
        }

        private static void HandlePilot(Pilot pilot, SimGameState s)
        {
            bool changedAnything = false;
            HandleSkill(pilot, s, "Piloting", pilot.pilotDef.SkillPiloting, ref changedAnything);
            HandleSkill(pilot, s, "Gunnery", pilot.pilotDef.SkillGunnery, ref changedAnything);
            HandleSkill(pilot, s, "Tactics", pilot.pilotDef.SkillTactics, ref changedAnything);
            HandleSkill(pilot, s, "Guts", pilot.pilotDef.SkillGuts, ref changedAnything);

            if (changedAnything)
            {
                pilot.pilotDef.ForceRefreshAbilityDefs();
                pilot.InitAbilities();
            }
        }

        private static void HandleSkill(Pilot pilot,SimGameState s, string ab, int val, ref bool changedAnything)
        {
            if (val < 1)
                return;
            if (!s.AbilityTree.ContainsKey(ab))
                return;
            if (val > s.AbilityTree[ab].Count)
                return;
            for (int slvl = 0; slvl < val; slvl++)
            {
                List<AbilityDef> l = s.AbilityTree[ab][slvl];
                foreach (AbilityDef a in l)
                {
                    if (a.IsPrimaryAbility && !pilot.pilotDef.abilityDefNames.Contains(a.Description.Id) && s.CanPilotTakeAbility(pilot.pilotDef, a, false))
                    {
                        pilot.pilotDef.abilityDefNames.Add(a.Description.Id);
                        changedAnything = true;
                    }
                }
            }
        }
    }
}
