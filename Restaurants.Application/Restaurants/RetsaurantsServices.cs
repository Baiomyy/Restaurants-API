
using AutoMapper;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants;

internal class RetsaurantsServices(IRestaurantsRepository restaurantsRepository,
    ILogger<RetsaurantsServices> logger,
    IMapper mapper) : IRetsaurantsServices
{
    public async Task<int> Create(CreateRestaurantDto dto)
    {
        logger.LogInformation("Creating a new restaurant with name: {Name}", dto.Name);
        var restaurant = mapper.Map<Restaurant>(dto); // Map CreateRestaurantDto to Restaurant entity
        int id = await restaurantsRepository.Create(restaurant);
        return id;
    }

    public async Task<IEnumerable<RestaurantDto>> GetAllRestaurants()
    {
        logger.LogInformation("Getting all restaurants");
        var restaurants = await restaurantsRepository.GetAllAsync();

        //var restaurantDtos = restaurants.Select(RestaurantDto.FromEntity);// = restaurant => RestaurantDto.FromEntity(restaurant)
        var restaurantDtos = mapper.Map<IEnumerable<RestaurantDto>>(restaurants);
        return restaurantDtos!; //maybe null, but we know it won't be null here, may be an empty collection

    }

    public async Task<RestaurantDto?> GetById(int id)
    {
        logger.LogInformation("Getting restaurant by id: {Id}", id);
        var restaurant = await restaurantsRepository.GetByIdAsync(id);
        //var restaurantDto = RestaurantDto.FromEntity(Restaurant);
        var restaurantDto = mapper.Map<RestaurantDto?>(restaurant);
        return restaurantDto;
    }
}
