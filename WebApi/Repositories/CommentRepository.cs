using Microsoft.EntityFrameworkCore;
using WebApi.Contexts;
using WebApi.DataTransferObjects;
using WebApi.Models.Entities;

namespace WebApi.Repositories;

public class CommentRepository
{
	private readonly DataContext _context;

	public CommentRepository(DataContext context)
	{
		_context = context;
	}

	public async Task<CommentHttpResponse> CreateAsync(CommentEntity entity)
	{
		entity.DateCreated = DateTime.Now;
		_context.Comments.Add(entity);
		await _context.SaveChangesAsync();

		return entity;
	}
}
