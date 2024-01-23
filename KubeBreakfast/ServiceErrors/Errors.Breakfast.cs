using ErrorOr;

namespace KubeBreakfast.ServiceErrors;

public static class Errors
{
    public static class Breakfast{
        public static Error NotFound => Error.NotFound(
            "Breakfast.NotFound",
            "Unfortunately, the breakfast with such 'id' not found"
        );

    }
}