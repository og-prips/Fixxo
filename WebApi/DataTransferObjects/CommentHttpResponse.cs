namespace WebApi.DataTransferObjects;

public class CommentHttpResponse
{
	public int CommentId { get; set; }
	public string CustomerName { get; set; } = null!;
	public string Email { get; set; } = null!;
	public string Comment { get; set; } = null!;
	public DateTime DateCreated { get; set; }
}
