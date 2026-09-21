using BepInEx;
using BepInEx.Logging;
using BepInEx.Configuration;

using R2API.Utils;

using RoR2.UI;

using UnityEngine;
using UnityEngine.UI;

using RiskOfOptions;
using RiskOfOptions.Options;
using RoR2;

namespace MorCrosshairs
{
    [BepInPlugin(
        "Liv.MorCrosshairs",
        "MorCrosshairs",
        "1.0.0"
    )]
    [NetworkCompatibility(
        CompatibilityLevel.NoNeedForSync,
        VersionStrictness.DifferentModVersionsAreOk
    )]

    [BepInDependency("com.bepis.r2api")]
    [BepInDependency("com.rune580.riskofoptions")] // Risk of Options
    public class Main : BaseUnityPlugin
    {
        private ManualLogSource logger;

        private static ConfigEntry<Color> CrosshairColor;
        private void Awake()
        {
            logger = Logger;
            Init(logger);

            On.RoR2.UI.CrosshairManager.UpdateCrosshair += CrosshairManager_UpdateCrosshair;
        }

        private void CrosshairManager_UpdateCrosshair(On.RoR2.UI.CrosshairManager.orig_UpdateCrosshair orig, CrosshairManager self, CharacterBody targetBody, Vector3 crosshairWorldPosition, Camera uiCamera)
        {
            logger.LogDebug("Initializing On.RoR2.UI.CrosshairManager.UpdateCrosshair : CrosshairManager_UpdateCrosshair.");
            
            orig(self, targetBody, crosshairWorldPosition, uiCamera);
            
            if (self.crosshairController == null) return;

            Graphic[] crosshairInstance = self.crosshairController.GetComponentsInChildren<Graphic>(true);

            foreach(Graphic element in crosshairInstance)
            {
                logger.LogInfo($"Setting {element} color.");
                element.color = CrosshairColor.Value;
            }

        }
        public void Init(ManualLogSource console)
        {
            console.LogInfo("Initializing setup!");

            RiskOfOptions_Setup();
        }

        private void RiskOfOptions_Setup()
        {
            ModSettingsManager.AddOption(new ColorOption(CrosshairColor = Config.Bind(
                "Crosshair Settings",
                "Color",
                Color.white,
                "Sets the crosshair color.")
            ));
        }
    }
}