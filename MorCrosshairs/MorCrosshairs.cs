using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;


using MorCrosshairs.Assets;
using MorCrosshairs.Events;
using MorCrosshairs.Utils;

using R2API.Utils;

using RoR2;
using RoR2.UI;

using UnityEngine;

namespace MorCrosshairs
{   
    
    [BepInPlugin(
        "OyasumiLiv.MorCrosshairs",
        "Mor' Crosshairs",
        "1.0.0"
    )]
    [NetworkCompatibility(CompatibilityLevel.NoNeedForSync,VersionStrictness.DifferentModVersionsAreOk)]
    [BepInDependency("com.bepis.r2api")]
    [BepInDependency("com.rune580.riskofoptions")] // Risk of Options
    public class Main : BaseUnityPlugin
    {
        public static ManualLogSource logger;
        private static ConfigEntry<Color> crosshairColor;
        private static ConfigEntry<float> crosshairScale;

        private void Awake()
        {
            logger = Logger;

            crosshairColor = Config.Bind(
                McConfig.Crosshair.General,
                McConfig.Crosshair.Colour.key = "Color",
                McConfig.Crosshair.Colour.color = Color.white,
                McConfig.Crosshair.Colour.description = "Sets the crosshair color."
            );
            
            crosshairScale = Config.Bind(
                McConfig.Crosshair.General,
                McConfig.Crosshair.Scale.key = "Scale",
                McConfig.Crosshair.Scale.thickness = 1.0f,
                McConfig.Crosshair.Scale.description = "Sets the crosshair size, multiplied relative to the original crosshair scale."
            );

            On.RoR2.UI.CrosshairManager.UpdateCrosshair += CrosshairManagerOnUpdateCrosshair;
            
            Init(logger);
        }

        private void CrosshairManagerOnUpdateCrosshair(On.RoR2.UI.CrosshairManager.orig_UpdateCrosshair orig, CrosshairManager self, CharacterBody targetBody, Vector3 crosshairWorldPosition, Camera uiCamera)
        {
            McUpdateCrosshair updateCrosshair = new McUpdateCrosshair();
            updateCrosshair.CrosshairManager_UpdateCrosshair(orig, self, targetBody, crosshairWorldPosition, uiCamera,
                crosshairColor.Value, crosshairScale.Value);
        }

        private void Init(ManualLogSource console)
        {
            console.LogInfo("Initializing setup!");
            
            McConfig.RiskOfOptions_Get("Allows you to customize the survivors' crosshairs.", BuildAssets.GetIcon());
            
            McConfig.RiskOfOptions_Get(crosshairColor, logger);
            
            McConfig.RiskOfOptions_Get(crosshairScale, logger);
        }
    }
}

