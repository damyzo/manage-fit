﻿using System;
namespace Contracts.Requests.Client
{
    public class AddClientRequest(
        string name,
        float weight,
        float height,
        string email)
    {
        public required string Name { get; set; } = name;

        public float Weight { get; set; } = weight;

        public float Height { get; set; } = height;

        public required string Email { get; set; } = email;
    }
}