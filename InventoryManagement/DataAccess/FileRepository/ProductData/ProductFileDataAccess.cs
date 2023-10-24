using Entities;

namespace DataAccess.FileRepository.ProductData;

public class ProductFileDataAccess : FileDataAccess<ProductItem>
{
    private const string FilePath = "C:\\Users\\LENOVO\\Downloads\\products.json";
    public ProductFileDataAccess(): base(FilePath) { }
}
