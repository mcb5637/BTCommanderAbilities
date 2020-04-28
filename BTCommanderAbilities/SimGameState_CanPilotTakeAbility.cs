using BattleTech;
using Harmony;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BTCommanderAbilities
{
    [HarmonyPatch(typeof(SimGameState), "CanPilotTakeAbility")]
    class SimGameState_CanPilotTakeAbility
    {
        public static void Postfix(PilotDef p, ref bool __result)
        {
            if (p.PilotTags != null && p.PilotTags.Contains("commander_player"))
            {
                __result = true;
            }
        }
    }
}
