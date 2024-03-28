using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFMtgApp.BusinessLogic
{
    public class CardData
    {
        public Card[]? Cards { get; set; }
    }
    public class Card
    {
        public string? Artist { get; set; }
        public string? Set_Name { get; set; }
        public int Set_Num { get; set; }
        public string? Color { get; set; }
        public string? Image { get; set; }
        public int Cost { get; set; }
        public bool Inkable { get; set; }
        public string Name { get; set; }
        public string? Type { get; set; }
        public string? Rarity { get; set; }
        public string? Flavor_Text { get; set; }
        public int Card_Num { get; set; }
        public string? Body_Text { get; set; }
        public string? Set_ID { get; set; }
        public string? Classifications { get; set; }
        public int Lore { get; set; }
        public int Willpower { get; set; }
        public int Strength { get; set; }
        public int Move_Cost { get; set; }
        public string? Abilities { get; set; }
        public string? Card_Variants { get; set; }
    }

}
