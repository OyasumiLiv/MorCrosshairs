using BepInEx.Configuration;
using BepInEx.Logging;
using RiskOfOptions;
using RiskOfOptions.OptionConfigs;
using RiskOfOptions.Options;

using UnityEngine;

namespace MorCrosshairs.Utils
{
    public static class McConfig
    {
        public readonly struct Crosshair
        {
            public static string General = "General";
            
            public readonly struct Colour
            {
                public static string key { get; set; }
                public static string description { get; set; }

                public static Color color { get; set; }
            }

            public readonly struct Scale
            {
                public static string key { get; set; }
                public static string description { get; set; }
                
                public static float thickness { get; set; }
            }
        }

        public static void RiskOfOptions_Get(string description, Sprite? icon)
        {
            ModSettingsManager.SetModDescription(description);
            if (icon != null) ModSettingsManager.SetModIcon(icon);
        }
        public static void RiskOfOptions_Get(ConfigEntry<Color> crosshairColor, ManualLogSource console)
        {
            RiskOfOptions_SetColor(crosshairColor, console);
        }
        
        public static void RiskOfOptions_Get(ConfigEntry<float> crosshairThickness, ManualLogSource console)
        {
            RiskOfOptions_SetScale(crosshairThickness, console);
        }
        
        private static void RiskOfOptions_SetColor(ConfigEntry<Color> crosshairColor, ManualLogSource? console = null)
        {
            ModSettingsManager.AddOption(new ColorOption(crosshairColor));
            console?.LogInfo($"Risk Of Options: Set general crosshair color to {crosshairColor.BoxedValue}");
        }

        private static void RiskOfOptions_SetScale(ConfigEntry<float> crosshairScale, ManualLogSource? console = null)
        {
            ModSettingsManager.AddOption(new SliderOption(crosshairScale, new StepSliderConfig
            {
                min = 0.5f,
                max = 3.0f,
                increment = 0.1f
            }));
            
            console?.LogInfo($"Risk Of Options: Set general crosshair scale to {crosshairScale.BoxedValue}");
        }
        
    }
}