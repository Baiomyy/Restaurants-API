﻿using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant;

public class CreateRestaurantCommandHandler(ILogger<CreateRestaurantCommandHandler> logger,
    IMapper mapper,
    IRestaurantsRepository restaurantsRepository) : IRequestHandler<CreateRestaurantCommand, int>
{
    public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating a new restaurant {@Restaurant}", request);// @ is used to log the object in a readable format(Serialization for Serilog)
        var restaurant = mapper.Map<Restaurant>(request); // Map CreateRestaurantDto to Restaurant entity
        int id = await restaurantsRepository.Create(restaurant);
        return id;
    }
}
