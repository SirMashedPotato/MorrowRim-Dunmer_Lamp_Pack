using HarmonyLib;
using Verse;
using System.Reflection;

namespace Mashed_ColourableLantern
{
    [StaticConstructorOnStartup]
    public class Main
    {
        static Main()
        {
            var harmony = new Harmony("com.Mashed_ColourableLantern");
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
    }
}