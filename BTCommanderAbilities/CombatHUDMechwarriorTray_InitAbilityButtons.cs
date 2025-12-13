using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Harmony;
using BattleTech.UI;
using BattleTech;
using System.Reflection.Emit;
using System.Reflection;

namespace BTCommanderAbilities
{
    [HarmonyPatch(typeof(CombatHUDMechwarriorTray), "InitAbilityButtons")]
    class CombatHUDMechwarriorTray_InitAbilityButtons
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> code)
        {
            string search = "System.Collections.Generic.List`1[BattleTech.Ability] get_ActiveAbilities()";
            foreach (CodeInstruction i in code)
            {
                yield return i;
                if (i.opcode == OpCodes.Callvirt && i.operand.ToString().Equals(search))
                {
                    yield return new CodeInstruction(OpCodes.Call, AccessTools.DeclaredMethod(typeof(CombatHUDMechwarriorTray_InitAbilityButtons), "FixAbilities"));
                }
            }
        }

        public static List<Ability> FixAbilities(List<Ability> ab)
        {
            return ab.Take(3).ToList();
        }
    }
}
