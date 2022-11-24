using Verse;

namespace Mashed_ColourableLantern
{
    public class Comp_GlowColourMatcher : ThingComp
    {
		public CompProperties_GlowColourMatcher Props
		{
			get
			{
				return (CompProperties_GlowColourMatcher)this.props;
			}
		}

        public override void PostSpawnSetup(bool respawningAfterLoad)
        {
			compGlower = parent.TryGetComp<CompGlower>();
			UpdateColour();
		}

        public override void ReceiveCompSignal(string signal)
        {
			if (signal == "PowerTurnedOn" || signal == "PowerTurnedOff" || signal == "FlickedOn" || signal == "FlickedOff" || signal == "Refueled" || signal == "RanOutOfFuel" 
				|| signal == "ScheduledOn" || signal == "ScheduledOff" || signal == "MechClusterDefeated" || signal == "Hackend" || signal == "RitualTargetChanged" || signal == "CrateContentsChanged")
			{
				UpdateColour();
			}
		}

		public void UpdateColour()
        {
			if (compGlower != null)
			{
				MatchColour();
			}
        }

		public void MatchColour()
        {
			colorOverride = compGlower.GlowColor;
			colorOverride.a = 255;
            if (!Props.useColourTwo)
            {
				parent.SetColor(colorOverride.ToColor);
			}
			else
            {
				parent.Graphic.colorTwo = new UnityEngine.Color(colorOverride.r, colorOverride.g, colorOverride.b, colorOverride.a);
			}
		}

		public ColorInt colorOverride;

		public CompGlower compGlower;
	}
}
