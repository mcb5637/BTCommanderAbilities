using Harmony;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BTCommanderAbilities
{
    class BTCommanderAbilitiesInit
    {
        public static BTCommanderAbilitiesSettings Settings;

        public static void Init(string dir, string sett)
        {
            try
            {
                Settings = JsonConvert.DeserializeObject<BTCommanderAbilitiesSettings>(sett);
            }
            catch (Exception e)
            {
                FileLog.Log(e.Message);
                Settings = new BTCommanderAbilitiesSettings();
            }
            var harmony = HarmonyInstance.Create("com.github.mcb5637.BTCommanderAbilities");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }
}
