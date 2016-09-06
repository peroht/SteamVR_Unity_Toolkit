﻿// Controller Tooltips|Prefabs|0030
namespace VRTK
{
    using UnityEngine;

    /// <summary>
    /// This adds a collection of Object Tooltips to the Controller that give information on what the main controller buttons may do. To add the prefab, it just needs to be added as a child of the relevant controller e.g. `[CameraRig]/Controller (right)` would add the controller tooltips to the right controller.
    /// </summary>
    /// <remarks>
    /// If the transforms for the buttons are not provided, then the script will attempt to find the attach transforms on the default controller model in the `[CameraRig]` prefab.
    /// If no text is provided for one of the elements then the tooltip for that element will be set to disabled.
    /// There are a number of parameters that can be set on the Prefab which are provided by the `VRTK/Scripts/VRTK_ControllerTooltips` script which is applied to the prefab.
    /// </remarks>
    /// <example>
    /// `VRTK/Examples/029_Controller_Tooltips` displays two cubes that have an object tooltip added to them along with tooltips that have been added to the controllers.
    /// </example>
    public class VRTK_ControllerTooltips : MonoBehaviour
    {
        public enum TooltipButtons
        {
            TriggerTooltip,
            GripTooltip,
            TouchpadTooltip,
            AppMenuTooltip,
            None
        }

        [Tooltip("The text to display for the trigger button action.")]
        public string triggerText;
        [Tooltip("The text to display for the grip button action.")]
        public string gripText;
        [Tooltip("The text to display for the touchpad action.")]
        public string touchpadText;
        [Tooltip("The text to display for the application menu button action.")]
        public string appMenuText;
        [Tooltip("The colour to use for the tooltip background container.")]
        public Color tipBackgroundColor = Color.black;
        [Tooltip("The colour to use for the text within the tooltip.")]
        public Color tipTextColor = Color.white;
        [Tooltip("The colour to use for the line between the tooltip and the relevant controller button.")]
        public Color tipLineColor = Color.black;
        [Tooltip("The transform for the position of the trigger button on the controller (this is usually found in `Model/trigger/attach`.")]
        public Transform trigger;
        [Tooltip("The transform for the position of the grip button on the controller (this is usually found in `Model/lgrip/attach`.")]
        public Transform grip;
        [Tooltip("The transform for the position of the touchpad button on the controller (this is usually found in `Model/trackpad/attach`.")]
        public Transform touchpad;
        [Tooltip("The transform for the position of the app menu button on the controller (this is usually found in `Model/button/attach`.")]
        public Transform appMenu;

        private bool triggerInitialised = false;
        private bool gripInitialised = false;
        private bool touchpadInitialised = false;
        private bool appMenuInitialised = false;

        private GameObject[] buttonTooltips;

        /// <summary>
        /// The ToggleTips method will display the controller tooltips if the state is `true` and will hide the controller tooltips if the state is `false`. An optional `element` can be passed to target a specific controller tooltip to toggle otherwise all tooltips are toggled.
        /// </summary>
        /// <param name="state">The state of whether to display or hide the controller tooltips, true will display and false will hide.</param>
        /// <param name="element">The specific element to hide the tooltip on, if it is `TooltipButtons.None` then it will hide all tooltips. Optional parameter defaults to `TooltipButtons.None`</param>
        public void ToggleTips(bool state, TooltipButtons element = TooltipButtons.None)
        {
            if (element == TooltipButtons.None)
            {
                for (int i = 0; i < buttonTooltips.Length; i++)
                {
                    buttonTooltips[i].SetActive(state);
                }
            }
            else
            {
                buttonTooltips[(int)element].SetActive(state);
            }
        }

        private void Awake()
        {
            triggerInitialised = false;
            gripInitialised = false;
            touchpadInitialised = false;
            appMenuInitialised = false;
            InitialiseTips();
            buttonTooltips = new GameObject[4]
            {
                transform.FindChild(TooltipButtons.TriggerTooltip.ToString()).gameObject,
                transform.FindChild(TooltipButtons.GripTooltip.ToString()).gameObject,
                transform.FindChild(TooltipButtons.TouchpadTooltip.ToString()).gameObject,
                transform.FindChild(TooltipButtons.AppMenuTooltip.ToString()).gameObject,
            };
        }

        private void InitialiseTips()
        {
            foreach (var tooltip in GetComponentsInChildren<VRTK_ObjectTooltip>())
            {
                var tipText = "";
                Transform tipTransform = null;

                switch (tooltip.name.Replace("Tooltip", "").ToLower())
                {
                    case "trigger":
                        tipText = triggerText;
                        tipTransform = GetTransform(trigger, "trigger");
                        if (tipTransform != null)
                        {
                            triggerInitialised = true;
                        }
                        break;
                    case "grip":
                        tipText = gripText;
                        tipTransform = GetTransform(grip, "lgrip"); ;
                        if (tipTransform != null)
                        {
                            gripInitialised = true;
                        }
                        break;
                    case "touchpad":
                        tipText = touchpadText;
                        tipTransform = GetTransform(touchpad, "trackpad"); ;
                        if (tipTransform != null)
                        {
                            touchpadInitialised = true;
                        }
                        break;
                    case "appmenu":
                        tipText = appMenuText;
                        tipTransform = GetTransform(appMenu, "button"); ;
                        if (tipTransform != null)
                        {
                            appMenuInitialised = true;
                        }
                        break;
                }

                tooltip.displayText = tipText;
                tooltip.drawLineTo = tipTransform;

                tooltip.containerColor = tipBackgroundColor;
                tooltip.fontColor = tipTextColor;
                tooltip.lineColor = tipLineColor;

                tooltip.Reset();

                if (tipText.Trim().Length == 0)
                {
                    tooltip.gameObject.SetActive(false);
                }
            }
        }

        private bool TipsInitialised()
        {
            return (triggerInitialised && gripInitialised && touchpadInitialised && appMenuInitialised);
        }

        private Transform GetTransform(Transform setTransform, string findTransform)
        {
            Transform returnTransform = null;
            if (setTransform)
            {
                returnTransform = setTransform;
            }
            else
            {
                returnTransform = transform.parent.FindChild("Model/" + findTransform + "/attach");
            }

            return returnTransform;
        }

        private void Update()
        {
            if (!TipsInitialised())
            {
                InitialiseTips();
            }
        }
    }
}