namespace ECommerce.Core.Result
{
    public class ApiResponse<T>
    {
        public T Data { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        // Başarılı response için
        public static ApiResponse<T> Success(T data, string message = "")
        {
            return new ApiResponse<T>
            {
                Data = data,
                IsSuccess = true,
                Message = message
            };
        }

        // Başarısız response için
        public static ApiResponse<T> Fail(string message)
        {
            return new ApiResponse<T>
            {
                IsSuccess = false,
                Message = message
            };
        }
    }
} 