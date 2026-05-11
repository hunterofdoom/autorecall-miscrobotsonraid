using HarmonyLib;
using RimWorld;
using Verse;
using AIRobot;
using System.Collections.Generic;
using System.Linq;

namespace AutoRecall_MiscRobots
{
    // =========================================================
    // Punto de entrada del mod: aplica todos los parches Harmony
    // =========================================================
    [StaticConstructorOnStartup]
    public static class AutoRecall_Startup
    {
        static AutoRecall_Startup()
        {
            var harmony = new Harmony("audur.autorecall.miscrobots");
            harmony.PatchAll();
            Log.Message("[AutoRecall] Mod cargado: los robots volverán automáticamente a sus estaciones al detectar un ataque.");
        }
    }

    // =========================================================
    // Lógica central: recall de todos los robots en el mapa
    // =========================================================
    public static class AutoRecall_Logic
    {
        /// <summary>
        /// Llama a Notify_CallBotForShutdown() en todas las estaciones de recarga
        /// del mapa dado. Equivalente a presionar "Recall All" en cada estación.
        /// </summary>
        public static void RecallAllRobotsOnMap(Map map)
        {
            if (map == null) return;

            List<X2_Building_AIRobotRechargeStation> stations =
                map.listerThings.AllThings
                   .OfType<X2_Building_AIRobotRechargeStation>()
                   .ToList();

            if (stations.Count == 0) return;

            Messages.Message("Robots Recalled. Danger.", MessageTypeDefOf.CautionInput);

            foreach (var station in stations)
            {
                // Mismo método que llama el botón "Recall All"
                station.Notify_CallBotForShutdown();
            }
        }
    }

    // =========================================================
    // PARCHE 1: IncidentWorker_RaidEnemy (raids enemigas humanas)
    // =========================================================
    [HarmonyPatch(typeof(IncidentWorker_RaidEnemy), "TryExecuteWorker")]
    public static class Patch_RaidEnemy
    {
        public static void Postfix(IncidentParms parms, bool __result)
        {
            if (!__result) return;
            AutoRecall_Logic.RecallAllRobotsOnMap(parms.target as Map);
        }
    }

    // =========================================================
    // PARCHE 2: IncidentWorker_MechCluster (enjambres mecánicos)
    // =========================================================
    [HarmonyPatch(typeof(IncidentWorker_MechCluster), "TryExecuteWorker")]
    public static class Patch_MechCluster
    {
        public static void Postfix(IncidentParms parms, bool __result)
        {
            if (!__result) return;
            AutoRecall_Logic.RecallAllRobotsOnMap(parms.target as Map);
        }
    }

    // =========================================================
    // PARCHE 3: IncidentWorker_Infestation (infestaciones de insectos)
    // =========================================================
    [HarmonyPatch(typeof(IncidentWorker_Infestation), "TryExecuteWorker")]
    public static class Patch_Infestation
    {
        public static void Postfix(IncidentParms parms, bool __result)
        {
            if (!__result) return;
            AutoRecall_Logic.RecallAllRobotsOnMap(parms.target as Map);
        }
    }

}
