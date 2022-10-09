using Verse;

namespace Mashed_ColourableLantern
{
    public class BuildingProperties : DefModExtension
    {
        public bool preventDuplicatePlacement = true;

        public static BuildingProperties Get(Def def)
        {
            return def.GetModExtension<BuildingProperties>();
        }
    }
}
