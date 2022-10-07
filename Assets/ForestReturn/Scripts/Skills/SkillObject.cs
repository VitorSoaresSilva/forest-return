using UnityEngine;

namespace ForestReturn.Scripts.Skills
{
    [CreateAssetMenu(fileName = "new skill", menuName = "Skills")]
    public class SkillObject : ScriptableObject
    {
        public int level;
        public int[] costLevelup;
        public string skillName;
        public string description;
    }
}