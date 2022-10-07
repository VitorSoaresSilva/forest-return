using _Developers.Vitor.Scripts.Utilities;
using ForestReturn.Scripts.Skills;

namespace ForestReturn.Scripts.Managers
{
    public class SkillManager : Singleton<SkillManager>
    {
        public const int MaxLevel = 3;
        public SkillObject simpleAttack;
        public SkillObject rangedAttack;
        public SkillObject spikeLineSkill;
        public SkillObject spikeCircleSkill;
        // public int simpleAttackLevel;
        // public int rangedAttackLevel;
        // public int spikeLineSkillLevel;
        // public int spikeCircleSkillLevel;

        // public void Load(SkillManagerData skillManagerData)
        // {
        //     if (skillManagerData == null)
        //     {
        //         skillManagerData = new SkillManagerData();
        //     }
        //     simpleAttack.level = skillManagerData.SimpleAttackLevel;
        //     rangedAttack.level = skillManagerData.RangedAttackLevel;
        //     spikeLineSkill.level = skillManagerData.SpikeLineSkillLevel;
        //     spikeCircleSkill.level = skillManagerData.SpikeCircleSkillLevel;
        // }

        public void Save()
        {
            // var data = new SkillManagerData(simpleAttackLevel, rangedAttackLevel, spikeLineSkillLevel,
            //     spikeCircleSkillLevel);
            //TODO: Save data
        }

        // public bool CanUpdateSimpleAttack()
        // {
        //     return simpleAttackLevel <= MaxLevel;
        // }
        
        // quanto custa pra upar
        
    }


    // public class SkillManagerData
    // {
    //     public int SimpleAttackLevel;
    //     public int RangedAttackLevel;
    //     public int SpikeLineSkillLevel;
    //     public int SpikeCircleSkillLevel;
    //
    //     public SkillManagerData()
    //     {
    //         SimpleAttackLevel = 1;
    //         RangedAttackLevel = 1;
    //         SpikeLineSkillLevel = 0;
    //         SpikeCircleSkillLevel = 0; 
    //     }
    //
    //     public SkillManagerData(int simpleAttackLevel, int rangedAttackLevel, int spikeLineSkillLevel, int spikeCircleSkillLevel)
    //     {
    //         SimpleAttackLevel = simpleAttackLevel;
    //         RangedAttackLevel = rangedAttackLevel;
    //         SpikeLineSkillLevel = spikeLineSkillLevel;
    //         SpikeCircleSkillLevel = spikeCircleSkillLevel;
    //     }
    // }
}