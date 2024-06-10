namespace Shlinks.Responses;

public class ApiResponse
{
    public bool Success { get; set; }
}

public class ApiResponse<T> : ApiResponse
{
    public T Data { get; set; }
}
