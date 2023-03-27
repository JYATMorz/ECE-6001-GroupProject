﻿namespace GameFellowship.Data
{
    public interface IGameService
    {
        Task<Game[]> GetAllGameAsync();
        Task<Game> GetGameAsync(int id);
        Task<Game> GetGameAsync(string name);
        Task<string> GetGameIconAsync(string name);
        Task<string[]> GetGameNamesAsync(int count, string? prefix = null);
        Task<string[]> GetGameNamesAsync(int[] gameIDs);
    }
}