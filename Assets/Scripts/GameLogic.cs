using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Planets
{
    public class GameLogic : MonoBehaviour
    {
        public InputAction quitAction;
        public InputAction inventoryAction;

        private void Awake()
        {
            quitAction.performed += context => Application.Quit();
            inventoryAction.performed += context =>
            {
                var inventory = FindObjectOfType<InventorySystem>();
                FindObjectOfType<UIController>().Toggle(Panels.Inventory, inventory);
            };

            quitAction.Enable();
            inventoryAction.Enable();
        }

        // Start is called before the first frame update
        void Start()
        {
            // for (int i = 0; i < 100; i++)
            // {
            //     var child = Instantiate(tree, new Vector3((float) (_random.NextDouble() * 22 - 15), (float) (_random.NextDouble() * 16 - 11)), Quaternion.identity);
            //     child.transform.parent = GameObject.Find("InteractiveObjects").transform;
            // }
        }
    }
}
