namespace Packt.Shared
{
    public partial class Person
    {
        private string _favoritePrimaryColor;

        public string Origin
        {
            get
            {
                return $"{Name} was born on {HomePlanet}";
            }
        }

        public string Greeting => $"{Name} says 'Hello'";
        public int Age => System.DateTime.Now.Year - DateOfBirth.Year;
        public string FavoriteIcecream { get; set; }
        public string FavoritePrimaryColor{
            get{
                return _favoritePrimaryColor;
            }
            set{
                switch(value.ToLower()){
                    case "red":
                    case "green":
                    case "blue":
                        _favoritePrimaryColor = value;
                        break;
                    default:
                        throw new System.ArgumentException(
                            $"{value} is not a primary color. Choose from: red, green, blue.");

                }
            }
        }
    }
}