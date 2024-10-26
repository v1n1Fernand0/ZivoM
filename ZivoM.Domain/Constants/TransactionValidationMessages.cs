namespace ZivoM.Constants
{
    public static class TransactionValidationMessages
    {
        public const string AmountMustBeGreaterThanZero = "The amount must be greater than zero.";
        public const string DateCannotBeInTheFuture = "The date cannot be in the future.";
        public const string CategoryIdIsRequired = "CategoryId is required.";
        public const string UserIdIsRequired = "UserId is required.";
        public const string TransactionNotFound = "Transaction not found.";
    }
}
