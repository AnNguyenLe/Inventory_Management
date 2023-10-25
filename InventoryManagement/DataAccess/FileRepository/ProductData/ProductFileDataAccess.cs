using Entities;

namespace DataAccess.FileRepository.ProductData;

public class ProductFileDataAccess : FileDataAccess<ProductItem>
{
    public ProductFileDataAccess(): base(DataFolderPath.ProductDataFilePath) { }
}
