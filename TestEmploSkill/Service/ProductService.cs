using TestEmploSkill.Model.Entities;

namespace TestEmploSkill.Service
{
    public class ProductService : IProductService
    {
        private readonly List<Product> _products = new List<Product>();
        private int _nextId = 1;

        //Get all
        public List<Product> GetAllProducts()
        {
            return _products;
        }

        //Get detail
        public Product GetProductById(int id)
        {
            return _products.FirstOrDefault(p => p.Id == id);
        }

        //Add 
        public Product AddProduct(Product product)
        {
            var checkExistProduct = _products.FirstOrDefault(p => p.Id == product.Id);
            if (checkExistProduct == null)
            { 
                product.Id = _nextId++;
                _products.Add(product);
                return product;
            }
            return product;
        }

        //Update 
        public bool UpdateProduct(int id, Product data)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null) return false;

            product.Name = data.Name;
            product.Price = data.Price;
            return true;
        }

        //Delete
        public bool DeleteProduct(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null) return false;

            _products.Remove(product);
            return true;
        }
    }
}
