using DataAccess.Interfaces;
using Entities;
using Services.Constants;
using Services.Extensions;

namespace Services.Product
{
    public class ProductService: IProductService
    {
        private IDataAccess<ProductItem> _repository;

        public static bool IsAnyPropertyNullOrEmpty<T>(T item)
        {
            if (item is null)
            {
                return true;
            }

            foreach (var property in item.GetType().GetProperties())
            {
                var propertyValue = property.GetValue(item);
                if (string.IsNullOrEmpty(propertyValue?.ToString()))
                {
                    return true;
                }
            }
            return false;
        }
        public ProductService(IDataAccess<ProductItem> repository)
        {
            _repository = repository;
        }

        public ServiceResult<List<ProductItem>> GetProducts()
        {
            List<ProductItem> products;
            var data = _repository.GetAll();

            if(data is null)
            {
                products = new List<ProductItem>();
            }
            else
            {
                products = data;
            }
            return new ServiceResult<List<ProductItem>>(products);
        }

        public ServiceResult<ProductItem> AddProduct(ProductItem product)
        {
            var validatingResult = ValidateProductFields(product);
            if(validatingResult != ProcessStatus.APPROVED)
            {
                return new ServiceResult<ProductItem>(validatingResult);
            }

            var products = _repository.GetAll();

            products.Add(product);
            _repository.SaveAll(products);

            return new ServiceResult<ProductItem>(product);
        }

        public ServiceResult<List<ProductItem>> GetMatchedProducts(string searchText)
        {
            Predicate<ProductItem> predicate = DoesProductContains(searchText);
            return new ServiceResult<List<ProductItem>>(_repository.GetMatchedItems(predicate));
        }

        public ServiceResult<ProductItem> GetFirstMatchedProduct(Predicate<ProductItem> predicate)
        {
            var product = _repository.GetFirstMatch(predicate);
            if (product is null)
            {
                return new ServiceResult<ProductItem>("not found.");
            }
            return new ServiceResult<ProductItem>(product);
        }

        public ServiceResult<string> DeleteProduct(string productId)
        {
            try
            {
                _repository.Delete(product => product.Id == productId);
                return new ServiceResult<string>(productId, true);
            }
            catch (InvalidOperationException)
            {
                return new ServiceResult<string>($"Deleting fail. Cannot find {productId} in the inventory");
            }
        }

        public ServiceResult<ProductItem> UpdateProduct(string oldId, ProductItem updatedProduct)
        {
            var validatingResult = ValidateProductFields(updatedProduct);
            if (validatingResult != ProcessStatus.APPROVED)
            {
                return new ServiceResult<ProductItem>(validatingResult);
            }
            var products = _repository.GetAll();

            var index = products.FindIndex(p => p.Id == oldId);
            if(index == -1) {
                return new ServiceResult<ProductItem>($"Updating fail. Cannot find product to be updated in the inventory");
            }
            products[index] = updatedProduct;
            _repository.SaveAll(products);

            return new ServiceResult<ProductItem>(updatedProduct);
        }

        private string ValidateProductFields(ProductItem product)
        {
            if (string.IsNullOrEmpty(product.Id))
            {
                return ProcessStatus.PRODUCT_ADDING_FAIL_NULL_OR_EMPTY_ID;
            }

            var hasActiveId = _repository.GetFirstMatch(item => item.Id == product.Id) is not null;

            if (hasActiveId)
            {
                return ProcessStatus.PRODUCT_ADDING_FAIL_DUPLICATED_ID;
            }

            if (IsAnyPropertyNullOrEmpty(product))
            {
                return ProcessStatus.PRODUCT_FAIL_INPUTS_ALL_FIELDS_REQUIRED;
            }

            if (product.Quantity < 0)
            {
                return ProcessStatus.PRODUCT_FAIL_INPUTS_QUANTITY_NOT_ALLOW_BELOW_ZERO;
            }

            if (product.Price < 0)
            {
                return ProcessStatus.PRODUCT_FAIL_INPUTS_PRICE_NOT_ALLOW_BELOW_ZERO;
            }

            if (product.YearOfManufacture > DateTime.Now.Year)
            {
                return ProcessStatus.PRODUCT_FAIL_INPUTS_MFG_DATE_NOT_IN_FUTURE;
            }
            if (product.ExpDate < DateTime.Today)
            {
                return ProcessStatus.PRODUCT_FAIL_INPUTS_EXP_DATE_NOT_IN_PAST;
            }

            return ProcessStatus.APPROVED;
        }

        public ServiceResult<List<string>> GetProductCategories()
        {
            var products = _repository.GetAll();
            var catergories = ExtractCategories(products);
            return new ServiceResult<List<string>>(catergories);
        }

        public ServiceResult<List<string>> GetMatchedProductCategories(string searchText)
        {
            var products = _repository.GetAll();
            var catergories = ExtractCategories(products);
            var text = searchText.TransformToContinuousLowercase();
            var matchedCategories = catergories.FindAll(c => c.TransformToContinuousLowercase().Contains(text));
            return new ServiceResult<List<string>>(matchedCategories);
        }

        private Predicate<ProductItem> DoesProductContains(string s) {
            var str = s.TransformToContinuousLowercase();
            return item =>
            {
                List<string> values = new() {
                    item.Id, item.Name, item.Manufacturer,
                    item.YearOfManufacture.ToString(), item.Category,
                    item.Price.ToString(), item.ExpDate.ToString(),
                    item.Quantity.ToString(), item.UnitOfMeasurement
                };
                foreach (var v in values)
                {
                    var value = v.TransformToContinuousLowercase();
                    if (value.Contains(s))
                    {
                        return true;
                    }
                }
                return false;
            };
        }

        private List<string> ExtractCategories(List<ProductItem> products)
        {
            var catergories = new List<string>();
            if (products.Count != 0)
            {
                foreach (var product in products)
                {
                    if (!catergories.Contains(product.Category))
                    {
                        catergories.Add(product.Category);
                    }
                }
            }
            return catergories;
        }
    }
}