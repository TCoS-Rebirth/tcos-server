using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TCoSServer.Common;

namespace TCoSServer.GameServer.Network.Structures
{
  struct sd_character_stats_record : SBStruct
  {
    public int Body;
    public int Mind;
    public int Focus;
    public float Physique;
    public float Morale;
    public float Concentration;
    public int FameLevel;
    public int PePRank;
    public float RuneAffinity;
    public float SpiritAffinity;
    public float SoulAffinity;
    public float MeleeResistance;
    public float RangedResistance;
    public float MagicResistance;
    public int MaxHealth;
    public float PhysiqueRegeneration;
    public float PhysiqueDegeneration;
    public float MoraleRegeneration;
    public float MoraleDegeneration;
    public float ConcentrationRegeneration;
    public float ConcentrationDegeneration;
    public float HealthRegeneration;
    public float AttackSpeedBonus;
    public float MovementSpeedBonus;
    public float DamageBonus;
    public float CopyHealth;

    public void WriteTo (MessageWriter writer)
    {
      writer.Write (Body);
      writer.Write (Mind);
      writer.Write (Focus);
      writer.Write (Physique);
      writer.Write (Morale);
      writer.Write (Concentration);
      writer.Write (FameLevel);
      writer.Write (PePRank);
      writer.Write (RuneAffinity);
      writer.Write (SpiritAffinity);
      writer.Write (SoulAffinity);
      writer.Write (MeleeResistance);
      writer.Write (RangedResistance);
      writer.Write (MagicResistance);
      writer.Write (MaxHealth);
      writer.Write (PhysiqueRegeneration);
      writer.Write (PhysiqueDegeneration);
      writer.Write (MoraleRegeneration);
      writer.Write (MoraleDegeneration);
      writer.Write (ConcentrationRegeneration);
      writer.Write (ConcentrationDegeneration);
      writer.Write (HealthRegeneration);
      writer.Write (AttackSpeedBonus);
      writer.Write (MovementSpeedBonus);
      writer.Write (DamageBonus);
      writer.Write (CopyHealth);
    }
  }
}
