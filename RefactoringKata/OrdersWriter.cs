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

            sb.Append(PrintOrder());

            return sb.Append("]}").ToString();
        }

        private string PrintOrder()
        {
            var orderDetail = string.Empty;
            for (var i = 0; i < _orders.GetOrdersCount(); i++)
            {
                var order = _orders.GetOrder(i);
                orderDetail += "{\"id\": ";
                orderDetail += order.GetOrderId();
                orderDetail += ", \"products\": [";
                orderDetail += PrintProducts(order);
                orderDetail += "]}, ";
            }

            if (_orders.GetOrdersCount() > 0)
            {
                orderDetail = orderDetail.Substring(0, orderDetail.Length - 2);
            }
            return orderDetail;
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