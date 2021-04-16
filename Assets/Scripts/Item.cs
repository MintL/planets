using UnityEngine;

namespace Planets
{
    public class Item
    {
        public int Id { get; }
        public string Name { get; }
        public Sprite Sprite { get; }

        public Item(int id, string name, string image)
        {
            Id = id;
            Name = name;
            Sprite = Resources.Load<Sprite>("Items/" + image);
        }
    }
}
