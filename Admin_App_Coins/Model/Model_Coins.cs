namespace Admin_App_Coins.Model
{
    public class Model_Coins
    {
        public int Id_Coins { get; set; }
        public int Id_Category { get; set; }

        public string Icon { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Count { get; set; }
        public int Year { get; set; }
        public string Mintage { get; set; }
        public string Composition { get; set; }
        public double Weight { get; set; }
        public double Diameter { get; set; }
        public double Thickness { get; set; }
        public string Edge_type { get; set; }
        public string Information { get; set; }
    }
}
