namespace sma_visualization
{
    internal class Item
    {
        public string Name { get; set; }
        public string Sector { get; set; }
        
        public string Symbol { get; set; }
        public Item()
        {

        }
        public Item(string name, string sector, string symbol)
        {
            this.Name = name;
            this.Sector = sector;
            this.Symbol = symbol;
        }
    }
}