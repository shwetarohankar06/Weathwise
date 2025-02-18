namespace WealthWiseAPI.Models
{
    public class CoinData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public double Price { get; set; }
        public double MarketCap { get; set; }
        public double Volume { get; set; }
        public double Change24h { get; set; }

    }


}
