using System.Collections.Generic;

namespace RefactoringKata
{
    public class Product
    {
        public static int SIZE_NOT_APPLICABLE = -1;

        public string Code { get; set; }
        public int Color { get; set; }
        public int Size { get; set; }
        public double Price { get; set; }
        public string Currency { get; set; }

        public Product(string code, int color, int size, double price, string currency)
        {
            Code = code;
            Color = color;
            Size = size;
            Price = price;
            Currency = currency;
        }

        public Dictionary<string, object> GetSpecifications()
        {
            var specifications = new Dictionary<string, object>()
            {
                {"code", Code},
                {"color", getColor()},
                {"size", getSize()},
                {"price", Price},
                {"currency", Currency}
            };
            if (Size == SIZE_NOT_APPLICABLE)
            {
                specifications.Remove("size");
            }
            return specifications;
        }

        private string getColor()
        {
            switch (Color)
            {
                case 1:
                    return "blue";
                case 2:
                    return "red";
                case 3:
                    return "yellow";
                default:
                    return "no color";
            }
        }


        private string getSize()
        {
            switch (Size)
            {
                case 1:
                    return "XS";
                case 2:
                    return "S";
                case 3:
                    return "M";
                case 4:
                    return "L";
                case 5:
                    return "XL";
                case 6:
                    return "XXL";
                default:
                    return "Invalid Size";
            }
        }

    }
}
