using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TGS {
    public class Demo27 : MonoBehaviour {

        TerrainGridSystem tgs;

        void Start() {
            tgs = TerrainGridSystem.instance;

            tgs.TerritoryHideInteriorBorders();
            tgs.OnTerritoryEnter += Tgs_OnTerritoryEnter;
            tgs.OnTerritoryExit += Tgs_OnTerritoryExit;
        }

        private void Tgs_OnTerritoryExit(TerrainGridSystem tgs, int territoryIndex) {
            tgs.TerritoryHideInteriorBorders();
        }

        private void Tgs_OnTerritoryEnter(TerrainGridSystem tgs, int territoryIndex) {

            tgs.TerritoryHideInteriorBorders();
            Territory territory = tgs.territories[territoryIndex];
            Color darkerColor = territory.fillColor;
            darkerColor.r *= 0.37f;
            darkerColor.g *= 0.85f;
            darkerColor.b *= 0.35f;
            tgs.TerritoryDrawInteriorBorder(territory, color: darkerColor, secondColor: territory.fillColor, animationSpeed: 2f, thickness: 5f);
        }

    }

}