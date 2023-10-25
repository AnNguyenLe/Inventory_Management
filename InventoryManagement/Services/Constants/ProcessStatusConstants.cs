namespace Services.Constants;

public class ProcessStatus
{
    public static readonly string APPROVED = "approved";
    
    public static readonly string PRODUCT_ADDING_FAIL_DUPLICATED_ID = "This ProductID had been registered already. Please choose another ProductID.";
    public static readonly string PRODUCT_ADDING_FAIL_NULL_OR_EMPTY_ID = "ProductID cannot be Null or Empty. Please enter a value for ProductID.";

    public static readonly string PRODUCT_FAIL_INPUTS_ALL_FIELDS_REQUIRED = "Please make sure: All fields are NOT left empty or null";
    public static readonly string PRODUCT_FAIL_INPUTS_QUANTITY_NOT_ALLOW_BELOW_ZERO = "Please make sure: Quantity cannot be less than 0.";
    public static readonly string PRODUCT_FAIL_INPUTS_PRICE_NOT_ALLOW_BELOW_ZERO = "Please make sure: Price cannot be less than 0.";
    public static readonly string PRODUCT_FAIL_INPUTS_MFG_DATE_NOT_IN_FUTURE = $"Today is {DateTime.Today.ToShortDateString()}. Please make sure: Manufacturing Date cannot be in the future.";
    public static readonly string PRODUCT_FAIL_INPUTS_EXP_DATE_NOT_IN_PAST = $"Today is {DateTime.Today.ToShortDateString()}. Please make sure: Exp Date must be in the future.";

    public static readonly string PRODUCT_CATEGORY_ADDING_FAIL_DUPLICATED_PRODUCT_CATEGORY = "This Product Category had been registered already. Please enter new Product Category.";
    public static readonly string PRODUCT_CATEGORY_ADDING_FAIL_PRODUCT_CATEGORY_CANNOT_BE_NULL_OR_EMPTY = "New product category cannot be null or empty.";
    public static readonly string PRODUCT_CATEGORY_UPDATING_FAIL_NO_PRODUCT = "Failed updating product(s) due to list of to be updated product(s) is empty.";
    public static readonly string PRODUCT_CATEGORY_UPDATING_SUCCESS = "Successfully updated products.";
    public static readonly string PRODUCT_CATEGORY_UPDATING_FAIL_CATEGORY_NULL_OR_EMPTY = "Product Category cannot be null or empty";
    public static readonly string PRODUCT_CATEGORY_UPDATING_FAIL_NO_DIFFERENCE_BETWEEN_CURRENT_AND_UPDATE_CATEGORY_NAME = "No difference between current and new category name.";

    public static readonly string PRODUCT_CATEGORY_DELETING_FAIL_NO_PRODUCT = "Failed deleting product(s) due to list of to be deleted product(s) is empty.";
    public static readonly string PRODUCT_CATEGORY_DELETING_SUCCESS = "Successfully updated products.";

    public static readonly string DOCUMENT_NOT_FOUND = "Document not found. Make sure the document ID is correct.";
    public static readonly string DOCUMENT_UPDATING_SUCCESS = "Successfully update document.";
    public static readonly string DOCUMENT_DELETING_SUCCESS = "Successfully delete document.";
}
