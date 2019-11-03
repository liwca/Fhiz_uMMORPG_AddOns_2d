using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class ScriptableSkill
{
    // Create our skill types to match our weapon types.
    public enum SkillType { None, Unarmed, Slash1Hand, Pierce1Hand, Bludgeon1Hand, Slash2Hand, Pierce2Hand, Bludgeon2Hand, RangedThrown, RangedBow, RangedGun, Shield, Dual, Cloth, Leather, Plate }

    // Create a selectable type for each skill.
    [Header("UCE Combat Remastered - Required Type")]
    public SkillType skillType = SkillType.None;
}