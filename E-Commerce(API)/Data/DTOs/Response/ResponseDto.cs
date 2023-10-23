using E_Commerce_API_.Data.DTOs;

namespace E_Commerce_API_.Data.DTOs.Error;

public class ResponseDto<T>
{
    public T Response { get; private set; }
    public List<LinkDto> Links { get; private set; }

    public ResponseDto(T obj, List<LinkDto> links)
    {
        Response = obj;
        Links = links;
    }
}
