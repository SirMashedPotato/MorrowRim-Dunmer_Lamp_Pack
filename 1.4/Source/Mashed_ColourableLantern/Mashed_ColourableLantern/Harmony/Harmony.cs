using HarmonyLib;
using Verse;
using System.Reflection;
using RimWorld;
using System.Collections.Generic;

namespace Mashed_ColourableLantern
{
    [StaticConstructorOnStartup]
    public class Main
    {
        static Main()
        {
            Harmony harmony = new Harmony("com.Mashed_ColourableLantern");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }

    [HarmonyPatch(typeof(CompGlower))]
    [HarmonyPatch("SetGlowColorInternal")]
    public static class CompGlower_SetGlowColorInternal_Patch
    {
        [HarmonyPostfix]
        public static void Mashed_ColourableLantern_Patch(ref CompGlower __instance)
        {
            Comp_GlowColourMatcher comp = __instance.parent.TryGetComp<Comp_GlowColourMatcher>();
            if (comp != null)
            {
                comp.UpdateColour();
            }
        }


        [HarmonyPatch(typeof(GenConstruct), "CanPlaceBlueprintAt")]
        public static class GenConstruct_CanPlaceBlueprintOver_Patch
        {
            public static void Postfix(ref AcceptanceReport __result, BuildableDef entDef, IntVec3 center, Rot4 rot, Map map)
            {
                if (BuildingProperties.Get(entDef) != null && BuildingProperties.Get(entDef).preventDuplicatePlacement)
                {
                    foreach (IntVec3 item in GenAdj.OccupiedRect(center, rot, entDef.Size))
                    {
                        List<Thing> thingList = item.GetThingList(map);
                        for (int i = 0; i < thingList.Count; i++)
                        {
                            Thing thing2 = thingList[i];
                            if (BuildingProperties.Get(thing2.def) != null && BuildingProperties.Get(thing2.def).preventDuplicatePlacement)
                            {
                                __result = new AcceptanceReport("SpaceAlreadyOccupied".Translate());
                                return;
                            }
                        }
                    }
                }
            }
        }
    }
}
