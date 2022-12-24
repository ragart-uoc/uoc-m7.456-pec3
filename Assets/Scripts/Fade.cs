using System;
using System.Collections;
using UnityEngine;

namespace PEC3
{
    /// <summary>
    /// Class <c>Fade</c> contains methods to help fading elements in and out.
    /// </summary>
    public static class Fade
    {
        /// <summary>
        /// Method <c>FadePropertyValue</c> fades the value of a certain property.
        /// </summary>
        public static IEnumerator FadePropertyValue(Component component, string property, float value, float time, Action callback)
        {
            // Get type of component
            var type = component.GetType();
            
            // Get property of component
            var prop = type.GetProperty(property);
            if (prop == null) yield break;
            
            // Get current value
            var currentValue = (float) prop.GetValue(component, null);
            
            // Check if value is higher or lower than current value
            if (currentValue > value)
            {
                while (currentValue > value)
                {
                    currentValue -= Time.deltaTime / time;
                    prop.SetValue(component, currentValue, null);
                    yield return null;
                }
            }
            else
            {
                while (currentValue < value)
                {
                    currentValue += Time.deltaTime / time;
                    prop.SetValue(component, currentValue, null);
                    yield return null;
                }
            }

            callback?.Invoke();
        }
        
        /// <summary>
        /// Method <c>FadeColorAlpha</c> fades the alpha value of the color property.
        /// </summary>
        public static IEnumerator FadeColorAlpha(Component component, float value, float time)
        {
            // Get type of component
            var type = component.GetType();
            
            // Get color property of component
            var color = type.GetProperty("color");
            if (color == null) yield break;
            
            // Get current color
            var currentColor = (Color) color.GetValue(component, null);
            
            // Set alpha to value depending on duration
            if (currentColor.a > value)
            {
                while (currentColor.a > value)
                {
                    currentColor.a -= Time.deltaTime / time;
                    color.SetValue(component, currentColor, null);
                    yield return null;
                }
            }
            else
            {
                while (currentColor.a < value)
                {
                    currentColor.a += Time.deltaTime / time;
                    color.SetValue(component, currentColor, null);
                    yield return null;
                }
            }
        }
    }
}
