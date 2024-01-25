using ErrorOr;

namespace KubeBreakfast.ServiceErrors;

public static class Errors
{
    public static class Breakfast{
        public static Error NotFound => Error.NotFound(
            "Breakfast.NotFound",
            "Unfortunately, the breakfast with such 'id' not found"
        );

        public static Error InvalidName => Error.Validation(
            "Breakfast.InvalidName",
            $"Name must be more than {Models.Breakfast.MinLengthForName} characters but less that {Models.Breakfast.MaxLengthForName}"
        );

        public static Error InvalidDescription => Error.Validation(
            "Breakfast.InvalidDescription",
            $"Description must be more than {Models.Breakfast.MinLengthForDescription} characters but less that {Models.Breakfast.MaxLengthForDescription}"
        );
    }
}