using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VelvetLeashAPI.Data;
using VelvetLeashAPI.DTOs;
using VelvetLeashAPI.Models;

namespace VelvetLeashAPI.Services
{
    public class PetSitterService : IPetSitterService
    {
        private readonly VelvetLeashDbContext _context;
        private readonly IMapper _mapper;

        public PetSitterService(VelvetLeashDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PetSitterDto> CreatePetSitterProfileAsync(string userId, CreatePetSitterDto createPetSitterDto)
        {
            var existingPetSitter = await _context.PetSitters
                .FirstOrDefaultAsync(ps => ps.UserId == userId);

            if (existingPetSitter != null)
            {
                throw new InvalidOperationException("Pet sitter profile already exists for this user");
            }

            var petSitter = _mapper.Map<PetSitter>(createPetSitterDto);
            petSitter.UserId = userId;
            petSitter.CreatedAt = DateTime.UtcNow;
            petSitter.UpdatedAt = DateTime.UtcNow;

            _context.PetSitters.Add(petSitter);
            await _context.SaveChangesAsync();

            // Add services if provided
            if (createPetSitterDto.Services != null && createPetSitterDto.Services.Any())
            {
                var services = createPetSitterDto.Services.Select(s => new Models.PetSitterService
                {
                    PetSitterId = petSitter.Id,
                    ServiceType = s.ServiceType,
                    Price = s.Price,
                    Description = s.Description,
                    CreatedAt = DateTime.UtcNow
                }).ToList();

                _context.PetSitterServices.AddRange(services);
                await _context.SaveChangesAsync();
            }

            return await GetPetSitterByIdAsync(petSitter.Id);
        }

        public async Task<PetSitterDto> GetPetSitterByIdAsync(int petSitterId)
        {
            var petSitter = await _context.PetSitters
                .Include(ps => ps.User)
                .Include(ps => ps.Services)
                .Include(ps => ps.Reviews)
                    .ThenInclude(r => r.Reviewer)
                .FirstOrDefaultAsync(ps => ps.Id == petSitterId);

            if (petSitter == null)
            {
                throw new KeyNotFoundException("Pet sitter not found");
            }

            return _mapper.Map<PetSitterDto>(petSitter);
        }

        public async Task<PetSitterDto> GetPetSitterByUserIdAsync(string userId)
        {
            var petSitter = await _context.PetSitters
                .Include(ps => ps.User)
                .Include(ps => ps.Services)
                .Include(ps => ps.Reviews)
                    .ThenInclude(r => r.Reviewer)
                .FirstOrDefaultAsync(ps => ps.UserId == userId);

            if (petSitter == null)
            {
                throw new KeyNotFoundException("Pet sitter profile not found");
            }

            return _mapper.Map<PetSitterDto>(petSitter);
        }

        public async Task<List<PetSitterDto>> SearchPetSittersAsync(BookingSearchDto searchDto)
        {
            var query = _context.PetSitters
                .Include(ps => ps.User)
                .Include(ps => ps.Services)
                .Include(ps => ps.Reviews)
                    .ThenInclude(r => r.Reviewer)
                .Where(ps => ps.IsAvailable);

            // Filter by location if provided
            if (!string.IsNullOrEmpty(searchDto.Location))
            {
                query = query.Where(ps => 
                    ps.City!.Contains(searchDto.Location) ||
                    ps.State!.Contains(searchDto.Location) ||
                    ps.ZipCode!.Contains(searchDto.Location));
            }

            // Filter by coordinates and radius if provided
            if (searchDto.Latitude.HasValue && searchDto.Longitude.HasValue)
            {
                var radiusInMiles = searchDto.RadiusInMiles ?? 25;
                // Note: This is a simplified distance calculation
                // In production, you'd use a proper geospatial query
                query = query.Where(ps => 
                    ps.Latitude.HasValue && ps.Longitude.HasValue &&
                    Math.Abs(ps.Latitude.Value - searchDto.Latitude.Value) <= (radiusInMiles * 0.0145) &&
                    Math.Abs(ps.Longitude.Value - searchDto.Longitude.Value) <= (radiusInMiles * 0.0145));
            }

            // Filter by service type if provided
            if (searchDto.ServiceType.HasValue)
            {
                query = query.Where(ps => ps.Services.Any(s => s.ServiceType == searchDto.ServiceType.Value && s.IsActive));
            }

            // Filter by price if provided
            if (searchDto.MaxPrice.HasValue)
            {
                query = query.Where(ps => 
                    ps.HourlyRate <= searchDto.MaxPrice ||
                    ps.DailyRate <= searchDto.MaxPrice ||
                    ps.OvernightRate <= searchDto.MaxPrice);
            }

            var petSitters = await query
                .OrderByDescending(ps => ps.AverageRating)
                .ThenByDescending(ps => ps.IsStarSitter)
                .Take(50)
                .ToListAsync();

            var result = _mapper.Map<List<PetSitterDto>>(petSitters);

            // Calculate distance if coordinates provided
            if (searchDto.Latitude.HasValue && searchDto.Longitude.HasValue)
            {
                foreach (var petSitter in result)
                {
                    if (petSitter.Latitude.HasValue && petSitter.Longitude.HasValue)
                    {
                        petSitter.DistanceInMiles = CalculateDistance(
                            searchDto.Latitude.Value, searchDto.Longitude.Value,
                            petSitter.Latitude.Value, petSitter.Longitude.Value);
                    }
                }

                result = result.OrderBy(ps => ps.DistanceInMiles).ToList();
            }

            return result;
        }

        public async Task<PetSitterDto> UpdatePetSitterAsync(string userId, UpdatePetSitterDto updatePetSitterDto)
        {
            var petSitter = await _context.PetSitters
                .FirstOrDefaultAsync(ps => ps.UserId == userId);

            if (petSitter == null)
            {
                throw new KeyNotFoundException("Pet sitter profile not found");
            }

            // Update only provided fields
            if (updatePetSitterDto.About != null)
                petSitter.About = updatePetSitterDto.About;
            
            if (updatePetSitterDto.Skills != null)
                petSitter.Skills = updatePetSitterDto.Skills;
            
            if (updatePetSitterDto.HomeDetails != null)
                petSitter.HomeDetails = updatePetSitterDto.HomeDetails;
            
            if (updatePetSitterDto.Address != null)
                petSitter.Address = updatePetSitterDto.Address;
            
            if (updatePetSitterDto.City != null)
                petSitter.City = updatePetSitterDto.City;
            
            if (updatePetSitterDto.State != null)
                petSitter.State = updatePetSitterDto.State;
            
            if (updatePetSitterDto.ZipCode != null)
                petSitter.ZipCode = updatePetSitterDto.ZipCode;
            
            if (updatePetSitterDto.Latitude.HasValue)
                petSitter.Latitude = updatePetSitterDto.Latitude;
            
            if (updatePetSitterDto.Longitude.HasValue)
                petSitter.Longitude = updatePetSitterDto.Longitude;
            
            if (updatePetSitterDto.IsAvailable.HasValue)
                petSitter.IsAvailable = updatePetSitterDto.IsAvailable.Value;
            
            if (updatePetSitterDto.HourlyRate.HasValue)
                petSitter.HourlyRate = updatePetSitterDto.HourlyRate;
            
            if (updatePetSitterDto.DailyRate.HasValue)
                petSitter.DailyRate = updatePetSitterDto.DailyRate;
            
            if (updatePetSitterDto.OvernightRate.HasValue)
                petSitter.OvernightRate = updatePetSitterDto.OvernightRate;

            petSitter.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return await GetPetSitterByIdAsync(petSitter.Id);
        }

        public async Task<bool> DeletePetSitterAsync(string userId)
        {
            var petSitter = await _context.PetSitters
                .FirstOrDefaultAsync(ps => ps.UserId == userId);

            if (petSitter == null)
            {
                return false;
            }

            _context.PetSitters.Remove(petSitter);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<ReviewDto> CreateReviewAsync(string reviewerId, CreateReviewDto createReviewDto)
        {
            var booking = await _context.Bookings
                .Include(b => b.PetSitter)
                .FirstOrDefaultAsync(b => b.Id == createReviewDto.BookingId);

            if (booking == null)
            {
                throw new KeyNotFoundException("Booking not found");
            }

            if (booking.UserId != reviewerId)
            {
                throw new UnauthorizedAccessException("You can only review bookings you made");
            }

            if (booking.Status != BookingStatus.Completed)
            {
                throw new InvalidOperationException("You can only review completed bookings");
            }

            var existingReview = await _context.Reviews
                .FirstOrDefaultAsync(r => r.BookingId == createReviewDto.BookingId && r.ReviewerId == reviewerId);

            if (existingReview != null)
            {
                throw new InvalidOperationException("You have already reviewed this booking");
            }

            var review = new Review
            {
                ReviewerId = reviewerId,
                RevieweeId = booking.PetSitter.UserId,
                BookingId = createReviewDto.BookingId,
                Rating = createReviewDto.Rating,
                Comment = createReviewDto.Comment,
                CreatedAt = DateTime.UtcNow
            };

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            // Update pet sitter's average rating
            await UpdatePetSitterRatingAsync(booking.PetSitterId);

            var reviewWithDetails = await _context.Reviews
                .Include(r => r.Reviewer)
                .FirstOrDefaultAsync(r => r.Id == review.Id);

            return _mapper.Map<ReviewDto>(reviewWithDetails);
        }

        public async Task<List<ReviewDto>> GetPetSitterReviewsAsync(int petSitterId)
        {
            var petSitter = await _context.PetSitters
                .FirstOrDefaultAsync(ps => ps.Id == petSitterId);

            if (petSitter == null)
            {
                throw new KeyNotFoundException("Pet sitter not found");
            }

            var reviews = await _context.Reviews
                .Include(r => r.Reviewer)
                .Where(r => r.RevieweeId == petSitter.UserId)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();

            return _mapper.Map<List<ReviewDto>>(reviews);
        }

        private async Task UpdatePetSitterRatingAsync(int petSitterId)
        {
            var petSitter = await _context.PetSitters
                .FirstOrDefaultAsync(ps => ps.Id == petSitterId);

            if (petSitter == null) return;

            var reviews = await _context.Reviews
                .Where(r => r.RevieweeId == petSitter.UserId)
                .ToListAsync();

            if (reviews.Any())
            {
                petSitter.AverageRating = reviews.Average(r => r.Rating);
                petSitter.TotalReviews = reviews.Count;
            }
            else
            {
                petSitter.AverageRating = 0;
                petSitter.TotalReviews = 0;
            }

            await _context.SaveChangesAsync();
        }

        private double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            // Haversine formula for calculating distance between two points
            const double R = 3959; // Earth's radius in miles

            var dLat = ToRadians(lat2 - lat1);
            var dLon = ToRadians(lon2 - lon1);

            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return R * c;
        }

        private double ToRadians(double degrees)
        {
            return degrees * Math.PI / 180;
        }
    }
}