using BepInEx.Logging;

using UnityEngine;
using UnityEngine.UI;

using RoR2;
using RoR2.UI;

namespace MorCrosshairs.Events
{
    public class McUpdateCrosshair : IEvents
    {
        private readonly ManualLogSource _logger = Main.logger;
        public void CrosshairManager_UpdateCrosshair(On.RoR2.UI.CrosshairManager.orig_UpdateCrosshair orig, CrosshairManager self, CharacterBody targetBody, Vector3 crosshairWorldPosition, Camera uiCamera, Color crosshairColor, float crosshairScale)
        {
            orig(self, targetBody, crosshairWorldPosition, uiCamera);
            
            if (self.crosshairController == null) return;
            
            Graphic[] hudColor = self.crosshairController.GetComponentsInChildren<Graphic>(true);
                
            SetCrosshairColor(hudColor, crosshairColor);
            SetCrosshairScale(self.crosshairController.rectTransform, crosshairScale);
            
        }
        
        private void SetCrosshairScale(RectTransform crosshairScale, float multiplier)
        {
            crosshairScale.localScale = Vector3.one * multiplier;
        }
        
        private void SetCrosshairColor(Graphic[] crosshairInstance, Color crosshairColor)
        {
            foreach(Graphic element in crosshairInstance)
            {
                element.color = crosshairColor;
            }
        }
    }
}