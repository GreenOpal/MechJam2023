using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MechJam {
    public class PartInfoHelper
    {
        private static Dictionary<int, Dictionary<Element, Dictionary<PartType, MechPartValues.PartValues>>> AllValuesMap;
        public static void Setup(MechPartValues parts)
        {
            AllValuesMap = new Dictionary<int, Dictionary<Element, Dictionary<PartType, MechPartValues.PartValues>>>();
            foreach (var tier in parts.Values)
            {
                AllValuesMap[tier.Tier] = new Dictionary<Element, Dictionary<PartType, MechPartValues.PartValues>>();
                foreach (var element in tier.Elements)
                {
                    AllValuesMap[tier.Tier][element.Element] = new Dictionary<PartType, MechPartValues.PartValues>();
                    foreach (var part in element.Parts)
                    {
                        AllValuesMap[tier.Tier][element.Element][part.Part] = part.Values;
                    }
                }
            }
        }

        public static MechPartValues.PartValues GetValues(int tier, Element element, PartType partType)
        {
            return AllValuesMap[tier][element][partType];
        }
    }
}
