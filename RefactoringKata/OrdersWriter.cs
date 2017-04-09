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
                sb.Append(PrintProducts(order));
                sb.Append("]");
                sb.Append("}, ");
            }

            if (_orders.GetOrdersCount() > 0)
            {
                sb.Remove(sb.Length - 2, 2);
            }

            return sb.Append("]}").ToString();
        }

        private string PrintProducts(Order order)
        {
            var productsDetail = string.Empty;
            for (var j = 0; j < order.GetProductsCount(); j++)
            {
                var product = order.GetProduct(j);
                var productSpecifications = product.GetSpecifications();
                productsDetail += PrintEachProductSpecification(productSpecifications);
            }
            return productsDetail;
        }

        private string PrintEachProductSpecification(Dictionary<string, object> productSpecifications)
        {
            var specifications = "{";
            var lastKey = productSpecifications.Last().Key;
            foreach (var productSpecification in productSpecifications)
            {
                specifications += string.Format("\"{0}\": {1}", productSpecification.Key,
                    PrintSpecificationValue(productSpecification.Value));
                if (productSpecification.Key != lastKey)
                {
                    specifications += ", ";
                }
            }
            specifications += "}";
            return specifications;
        }

        private string PrintSpecificationValue(object specificationValue)
        {
            var value = specificationValue.ToString();
            if (!IsNumber(specificationValue))
                value = "\"" + value + "\"";
            return value;
        }

        private static bool IsNumber(object specificationValue)
        {
            return specificationValue is double;
        }

    }
}