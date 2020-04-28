using Harmony;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BTCommanderAbilities
{
    class BTCommanderAbilitiesInit
    {
        public static void Init(string dir, string sett)
        {
            var harmony = HarmonyInstance.Create("com.github.mcb5637.BTCommanderAbilities");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }
}
