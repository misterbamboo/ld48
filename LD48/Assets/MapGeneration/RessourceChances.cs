using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.MapGeneration
{
    public class RessourceChances
    {
        // formula setup here
        // https://www.desmos.com/calculator/auw2vl9xkp
        private static Func<float, float> ressourceIronChanceFormula = (deepness) => -0.001f * Mathf.Pow(deepness - 70f, 2f) + 5f;
        private static Func<float, float> ressourceCopperChanceFormula = (deepness) => -0.01f * Mathf.Pow(deepness - 150f, 2f) + 15f;
        private static Func<float, float> ressourceGoldChanceFormula = (deepness) => -0.002f * Mathf.Pow(deepness - 200f, 2f) + 5f;
        private static Func<float, float> ressourcePlatinumChanceFormula = (deepness) => -0.0005f * Mathf.Pow(deepness - 350f, 2f) + 5f;
        private static Func<float, float> ressourceDiamondChanceFormula = (deepness) => Mathf.Pow(deepness, 1 / 2f) - 20.5f;

        private static Dictionary<MapCellType, Func<float, float>> formulaPerRessource = new Dictionary<MapCellType, Func<float, float>>()
        {
            { MapCellType.Iron, ressourceIronChanceFormula },
            { MapCellType.Copper, ressourceCopperChanceFormula },
            { MapCellType.Gold, ressourceGoldChanceFormula },
            { MapCellType.Platinum, ressourcePlatinumChanceFormula },
            { MapCellType.Diamond, ressourceDiamondChanceFormula },
        };

        public static IEnumerable<MapCellType> GetPossibilities(int deepness)
        {
            foreach (var formula in formulaPerRessource)
            {
                if(formula.Value(deepness) > 0)
                {
                    yield return formula.Key;
                }
            }
        }

        public static float GetChanceFor(MapCellType possibleRessource, int deepness)
        {
            var formula = formulaPerRessource[possibleRessource];
            return formula(deepness) / 100;
        }
    }
}
