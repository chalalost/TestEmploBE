using TestEmploSkill.Model.Entities;

namespace TestEmploSkill.Service
{
    public interface IProductService
    {
        List<Product> GetAllProducts();
        Product GetProductById(int id);
        Product AddProduct(Product product);
        bool UpdateProduct(int id, Product data);
        bool DeleteProduct(int id);

    }
}
