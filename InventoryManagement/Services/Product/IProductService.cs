using Entities;

namespace Services.Product;

public interface IProductService
{
    ServiceResult<List<ProductItem>> GetProducts();
    ServiceResult<ProductItem> AddProduct(ProductItem product);
    ServiceResult<List<ProductItem>> GetMatchedProducts(string searchText);
    ServiceResult<ProductItem> GetFirstMatchedProduct(Predicate<ProductItem> predicate);
    ServiceResult<string> DeleteProduct(string productId);
    ServiceResult<ProductItem> UpdateProduct(string oldId, ProductItem updatedProduct);
    ServiceResult<List<string>> GetProductCategories();
    ServiceResult<List<string>> GetMatchedProductCategories(string searchText);
}
