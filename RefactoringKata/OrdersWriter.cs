using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RefactoringKata
{
    public class OrdersWriter
    {
        private Orders _orders;

        public OrdersWriter(Orders orders)
        {
            _orders = orders;
        }

        public string GetContents()
        {
            var sb = new StringBuilder("{\"orders\": [");

            for (var i = 0; i < _orders.GetOrdersCount(); i++)
            {
                var order = _orders.GetOrder(i);
                sb.Append("{");
                sb.Append("\"id\": ");
                sb.Append(order.GetOrderId());
                sb.Append(", ");
                sb.Append("\"products\": [");

                PrintProducts(order, sb);

                sb.Append("]");
                sb.Append("}, ");
            }

            if (_orders.GetOrdersCount() > 0)
            {
                sb.Remove(sb.Length - 2, 2);
            }

            return sb.Append("]}").ToString();
        }

        private void PrintProducts(Order order, StringBuilder sb)
        {
            for (var j = 0; j < order.GetProductsCount(); j++)
            {
                var product = order.GetProduct(j);
                Dictionary<string, object> productSpecifications = new Dictionary<string, object>()
                {
                    {"code",product.Code},
                    {"color",getColorFor(product)},
                    {"size",getSizeFor(product)},
                    {"price",product.Price},
                    {"currency",product.Currency}
                };

                sb.Append("{");
                sb.Append("\"" + productSpecifications.ElementAt(0).Key + "\": \"" + productSpecifications.ElementAt(0).Value);
                sb.Append("\", ");
                sb.Append("\"" + productSpecifications.ElementAt(1).Key + "\": \"" + productSpecifications.ElementAt(1).Value);
                sb.Append("\", ");
                if (product.Size != Product.SIZE_NOT_APPLICABLE)
                {
                    sb.Append("\"" + productSpecifications.ElementAt(2).Key + "\": \"" + productSpecifications.ElementAt(2).Value);
                    sb.Append("\", ");
                }

                sb.Append("\"" + productSpecifications.ElementAt(3).Key + "\": " + productSpecifications.ElementAt(3).Value);
                sb.Append(", ");
                sb.Append("\"" + productSpecifications.ElementAt(4).Key + "\": \"" + productSpecifications.ElementAt(4).Value);
                sb.Append("\"}, ");

                if (order.GetProductsCount() > 0)
                {
                    sb.Remove(sb.Length - 2, 2);
                }
            }
        }

        private string getSizeFor(Product product)
        {
            switch (product.Size)
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

        private string getColorFor(Product product)
        {
            switch (product.Color)
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
    }
}