namespace SALESSYSTEM.BLL.Commons.Bases.Response
{
    public class BaseResponse<T>
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public IEnumerable<T>? ListObject { get; set; }
    }
}
