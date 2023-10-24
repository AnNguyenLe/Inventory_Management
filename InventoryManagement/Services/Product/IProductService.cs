using Entities;

namespace Services.Product;

public interface IProductService
{
    ServiceResult<List<ProductItem>> GetProducts();
    ServiceResult<ProductItem> AddProduct(ProductItem product);
    ServiceResult<List<ProductItem>> GetMatchedSearchProducts(string searchText);
    ServiceResult<ProductItem> GetFirstMatchedProduct(Predicate<ProductItem> predicate);
    ServiceResult<string> DeleteProduct(string productId);
    ServiceResult<ProductItem> UpdateProduct(string oldId, ProductItem updatedProduct);
    ServiceResult<List<string>> GetProductCategories();
    ServiceResult<List<string>> GetMatchedProductCategories(string searchText);
    ServiceResult<string> UpdateProductCategory(string oldCategory, string newCategory);
    ServiceResult<string> DeleteProductCategory(string productCategory);
    ServiceResult<List<ProductItem>> GetMatchedProducts(Predicate<ProductItem> predicate);
    ServiceResult<ProductItem> AddProductCategory(ProductItem product);

}
