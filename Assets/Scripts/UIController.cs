using UnityEngine;

namespace Planets
{
    public enum Panels
    {
        MiningDrill,
        Inventory,
        Recipe,
    }

    public class UIController : MonoBehaviour
    {
        public MiningDrillController MiningDrill;
        public InventoryController Inventory;
        public RecipeController RecipeController;

        private IPanel _current;

        public void Show(Panels panel, object element = null)
        {
            Hide();

            _current = panel switch
            {
                Panels.MiningDrill => MiningDrill as IPanel,
                Panels.Inventory => Inventory as IPanel,
                Panels.Recipe => RecipeController as IPanel,
            };

            if (_current is MonoBehaviour show)
            {
                var canvas = show.GetComponent<CanvasGroup>();
                canvas.alpha = 1;
                canvas.interactable = true;
                canvas.blocksRaycasts = true;

                _current.OnElementChanged(element);
                _current.OnShow();
            }
        }

        public void Hide()
        {
            if (_current is MonoBehaviour hide)
            {
                var canvas = hide.GetComponent<CanvasGroup>();
                canvas.alpha = 0;
                canvas.interactable = false;
                canvas.blocksRaycasts = false;

                _current.OnElementChanged(null);
                _current = null;
            }
        }

        public void Toggle(Panels panel, object element = null)
        {
            var togglePanel = panel switch
            {
                Panels.MiningDrill => MiningDrill as IPanel,
                Panels.Inventory => Inventory as IPanel,
                Panels.Recipe => RecipeController as IPanel,
            };

            if (_current != null)
            {
                Hide();
            }
            else
            {
                Show(panel, element);
            }
        }
    }
}
