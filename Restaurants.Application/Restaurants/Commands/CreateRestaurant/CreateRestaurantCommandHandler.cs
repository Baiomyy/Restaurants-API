using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant;

public class CreateRestaurantCommandHandler(ILogger<CreateRestaurantCommandHandler> logger,
    IMapper mapper,
    IRestaurantsRepository restaurantsRepository,
    IUserContext userContext) : IRequestHandler<CreateRestaurantCommand, int>
{
    public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();

        logger.LogInformation("{UserEmail} [{UserId}] is creating a new restaurant {@Restaurant}",
            currentUser.Email,
            currentUser.Id,
            request);// @ is used to log the object in a readable format(Serialization for Serilog)

        var restaurant = mapper.Map<Restaurant>(request); // Map CreateRestaurantDto to Restaurant entity
        restaurant.OwnerId = currentUser.Id; // Set the owner of the restaurant to the current user

        int id = await restaurantsRepository.Create(restaurant);
        return id;
    }
}
