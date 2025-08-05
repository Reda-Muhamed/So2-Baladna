﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using So2Baladna.Core.Dto;
using So2Baladna.Core.Entities;
using So2Baladna.Core.Entities.Product;
using So2Baladna.Core.Interfaces;
using So2Baladna.infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace So2Baladna.Infrastructure.Repositories
{
    public class RatingRepository:IRating
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public RatingRepository(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<bool> AddRatingAsync(RatingDto ratingDTO, string email)
        {
            var finduser = await _userManager.FindByEmailAsync(email);

            if (await _context.Ratings.AsNoTracking().AnyAsync(m => m.AppUserId == finduser.Id && m.ProductId == ratingDTO.productId))
            {
                return false;
            }

            var rating = new Rating()
            {
                AppUserId = finduser.Id,
                ProductId = ratingDTO.productId,
                Stars = ratingDTO.stars,
                content = ratingDTO.content,

            };
            await _context.Ratings.AddAsync(rating);
            await _context.SaveChangesAsync();

            var product = await _context.Products.FirstOrDefaultAsync(m => m.Id == ratingDTO.productId);

            var ratings = await _context.Ratings.AsNoTracking().Where(m => m.ProductId == product.Id).ToListAsync();

            if (ratings.Count > 0)
            {
                double average = ratings.Average(m => m.Stars);
                double roundedReview = Math.Round(average * 2, mode: MidpointRounding.AwayFromZero) / 2;
                product.rating = roundedReview;
            }
            else
            {
                product.rating = ratingDTO.stars;
            }
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<IReadOnlyList<ReturnRatingDto>> GetAllRatingForProductAsync(int productId)
        {
            var ratings = await _context.Ratings.Include(m => m.AppUser)
                 .AsNoTracking().Where(m => m.ProductId == productId).ToListAsync();

            return ratings.Select(m => new ReturnRatingDto
            {
                content = m.content,
                ReviewTime = m.Review,
                stars = m.Stars,
                userName = m.AppUser.UserName,
            }).ToList();
        }
    
}
}
