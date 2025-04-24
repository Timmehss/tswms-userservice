namespace TSWMS.UserService.Api.Dto;

public class ErrorResponse
{
    public string FriendlyMessage { get; set; }
    public string? StackTrace { get; set; }
}
